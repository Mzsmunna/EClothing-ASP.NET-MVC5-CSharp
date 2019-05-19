using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCEClothing.Models;

namespace MVCEClothing.Controllers
{
    public class LoginController : Controller
    {
        AllDataTableContext DB = new AllDataTableContext();
        // GET: Login
        public ActionResult Index()
        {
            /*if (DB.IsCookieEnabled() == false)
            {
                TempData["msg"] = "Your Cookie is Disabled!";
                return Redirect("/Index");
            }*/

            DB.PageStats("log1_Count");

            if ((Request.Cookies["userInfo"] != null) && (Request.Cookies["userInfo"]["username"] != "") && (Request.Cookies["userInfo"]["hashpassword"] != ""))
            {
                DB.CheckCookie();
            }
            else if (Session["usertype"] != null)
            {
                IsLogged();               
            }

            return View();
        }

        //[HttpPost]
        public ActionResult Index2()
        {
            /*if (DB.IsCookieEnabled() == false)
            {
                TempData["msg"] = "Your Cookie is Disabled!";
                return Redirect("/Index");
            }*/

            if (Session["timeduration"] != null)
            {
                DateTime currentTime = DateTime.Now;
                DateTime xMinsLater = DateTime.Parse(Session["timeduration"].ToString());

                if (String.Equals("3", Session["loginblock"].ToString()))
                {
                    TempData["pwderr_msg"] = "Seems like your account has been blocked! Please Retrieve your password 1st!";
                    return Redirect("/Login");
                }
                else if (currentTime.TimeOfDay <= xMinsLater.TimeOfDay)
                {
                    TempData["pwderr_msg"] = "Login Blocked! Try After : " + xMinsLater.ToString("HH:mm:ss tt");
                    return Redirect("/Login");
                }
                else
                {
                    Session["timeduration"] = null;
                    return Redirect("/Login");
                }
            }
            else if (Session["usertype"] != null)
            {
                IsLogged();
            }
            else if ((Request["usernameEmail"] == null) && (Session["username"] == null))
            {
                return Redirect("/Login");
            }
            else if (Request["usernameEmail"] == "")
            {
                Session["username_empty"] = "Username can't be empty";
                return Redirect("/Login");
            }
            else
            {
                if ((Session["username"] != null) && (Request["usernameEmail"] == null))
                {
                    if (Session["username"].ToString() != "")
                    {
                        return View();
                    }
                    else
                    {
                        return Redirect("/Login");
                    }
                }
                else
                {
                    string username = Request["usernameEmail"];

                    if (Session["username_err"] == null)
                    {
                        Session["username_err"] = 0;
                    }

                    if (DB.GetUser(username) == false)
                    {
                        int counter = Convert.ToInt32(Session["username_err"]);
                        Session["username_err"] = ++counter;

                        if (counter >= 3)
                        {
                            TempData["usrerr_msg"] = "Seems like you are not registered! Please Register 1st!";
                            return Redirect("/Register");                          
                        }
                        else
                        {
                            TempData["usrerr_msg"] = "Access Denied. username/email does not exist!";
                            return Redirect("/Login");
                        }
                    }
                    else
                    {
                        return View();
                    }

                }
                //return Redirect("/Login");
            }

            return Redirect("/Login");
        }

        [HttpPost]
        public ActionResult Logged()
        {
            /*if (DB.IsCookieEnabled() == false)
            {
                TempData["msg"] = "Your Cookie is Disabled!";
                return Redirect("/Index");
            }*/

            if (Session["timeduration"] != null)
            {
                DateTime currentTime = DateTime.Now;
                DateTime xMinsLater = DateTime.Parse(Session["timeduration"].ToString());

                if (String.Equals("3", Session["loginblock"].ToString()))
                {
                    TempData["pwderr_msg"] = "Seems like your account has been blocked! Please Retrieve your password 1st!";
                    return Redirect("/Login");

                }
                else if (currentTime.TimeOfDay <= xMinsLater.TimeOfDay)
                {
                    TempData["pwderr_msg"] = "Login Blocked! Try After : " + xMinsLater.ToString("HH:mm:ss tt");
                    return Redirect("/Login");
                }
                else
                {
                    Session["timeduration"] = null;
                    //this.Context.Items["password"] = Request["password"];
                    //Server.Transfer("login2.aspx", true);
                    return Redirect("/Login/Index2");
                }
            }
            else if (Session["usertype"] != null)
            {
                IsLogged();
            }
            else if (Session["username"] == null)
            {
                return Redirect("/Login");
            }
            else if (Request["password"] == null)
            {
                return Redirect("/Login/Index2");
            }
            else if (Request["password"] == "")
            {
                TempData["pwderr_msg"] = "Access Denied. Please Enter password!";
                return Redirect("/Login/Index2");
            }
            else
            {
                string username = Session["username"].ToString();
                string password = Request["password"];

                if (Session["password_err"] == null)
                {
                    Session["password_err"] = 0;
                }

                //string hashpassword = DB.HashPassword(password);


                //if (DB.Login(username, hashpassword) == true)
                if (DB.Login(username, password) == true)
                {
                    DB.PageStats("total_logged");
                    Session["password_err"] = 0;
                    Session["timeduration"] = null;

                    if (Request["remember"] != null)
                    {
                        Session["remember"] = Request["remember"];
                        DB.SetCookie(username, password);
                        //DB.SetCookie(username, hashpassword);
                    }
                    else
                    {
                        Session["remember"] = "Not remembered";
                    }

                    if ((Session["usertype"].ToString() == "admin") || (Session["usertype"].ToString() == "Admin"))
                    {
                        if(Session["password"].ToString().Equals(password))
                        {
                            //Session.Remove("password");
                            return Redirect("/Admin");
                        }
                        else
                        {
                            return Redirect("/Login");
                        }
                        //return Redirect("/Admin");
                    }
                    else if ((Session["usertype"].ToString() == "user") || (Session["usertype"].ToString() == "User"))
                    {
                        if (Session["password"].ToString().Equals(password))
                        {
                            //Session.Remove("password");
                            return Redirect("/User");
                        }
                        else
                        {
                            return Redirect("/Login");
                        }
                        //return Redirect("/User");
                    }
                }
                else
                {
                    int counter = Convert.ToInt32(Session["password_err"]);
                    Session["password_err"] = ++counter;

                    if (counter >= 3)
                    {
                        DateTime currentTime = DateTime.Now;
                        DateTime xMinsLater;

                        if (Session["loginblock"] == null)
                        {
                            Session["loginblock"] = 1;
                            xMinsLater = currentTime.AddMinutes(1);
                            Session["timeduration"] = xMinsLater.ToString("HH:mm:ss tt");
                            TempData["pwderr_msg"] = "3 times failure! Login Blocked for 5mins! Try After : " + xMinsLater.ToString("HH:mm:ss tt");
                            return Redirect("/Login");
                        }

                        if (counter == 6)
                        {
                            Session["loginblock"] = 2;
                            xMinsLater = currentTime.AddMinutes(2);
                            Session["timeduration"] = xMinsLater.ToString("HH:mm:ss tt");
                            TempData["pwderr_msg"] = "Again 3 times failure! Login Blocked for 15mins this time! Try After : " + xMinsLater.ToString("HH:mm:ss tt");
                            return Redirect("/Login");
                        }
                        else if (counter >= 9)
                        {
                            Session["loginblock"] = 3;
                            xMinsLater = currentTime.AddMinutes(60 * 24 * 365);
                            Session["timeduration"] = xMinsLater.ToString("HH:mm:ss tt");
                            TempData["pwderr_msg"] = "You just blocked your account! Too many Attempts!!! Seems like you Forget your Password! Please Retrieve your password 1st!";
                            return Redirect("/Forget");
                        }
                        else
                        {
                            TempData["pwderr_msg"] = "Error! Wrong Password!";
                            //return Redirect("/Login");
                            return Redirect("/Login/Index2");
                        }
                    }
                    else
                    {
                        TempData["pwderr_msg"] = "Error! Wrong Password!";
                        //return Redirect("/Login");
                        return Redirect("/Login/Index2");
                    }
                }
            }

            return Redirect("/User");
        }

        private ActionResult IsLogged()
        {
            if ((Session["usertype"].ToString() == "admin") || (Session["usertype"].ToString() == "Admin"))
            {
                return Redirect("/Admin");
            }
            else if ((Session["usertype"].ToString() == "user") || (Session["usertype"].ToString() == "User"))
            {
                return Redirect("/User");
            }
            else
            {
                return Redirect("/Login");
            }
        }
    }
}