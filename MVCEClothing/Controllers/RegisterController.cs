using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCEClothing.Models;

namespace MVCEClothing.Controllers
{
    public class RegisterController : Controller
    {
        AllDataTableContext DB = new AllDataTableContext();
        // GET: Register
        public ActionResult Index()
        {
            DB.PageStats("reg1_Count");

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

        public ActionResult Index2()
        {
            if (Session["usertype"] != null)
            {
                IsLogged();
            }
            else if ((Request["username"] == null) || (Request["firstname"] == null) || (Request["lastname"] == null) || (Request["gender"] == null) || (Request["DOB"] == null))
            {
                return Redirect("/Register");

            }
            else if ((Request["username"] == "") || (Request["firstname"] == "") || (Request["lastname"] == "") || (Request["gender"] == "") || (Request["DOB"] == ""))
            {
                TempData["regerr_msg"] = "Access Denied. Please fill up all blank boxes!";
                return Redirect("/Register");
            }
            else
            {
                string username = Request["username"];
                string firstname = Request["firstname"];
                string lastname = Request["lastname"];
                string gender = Request["gender"];
                string DOB = Request["DOB"].ToString();

                if (DB.CheckUsername(username) == false)
                {
                    Session["r_username"] = username;
                    Session["r_firstname"] = firstname;
                    Session["r_lastname"] = lastname;
                    Session["r_gender"] = gender;
                    Session["r_DOB"] = DOB;

                    return View();
                }
                else
                {
                    TempData["regerr_msg"] = "OOPS! This username already taken!";
                    return Redirect("/Register");
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Registered()
        {
            if ((Session["usertype"] != null) && (Request["usertype"] == null))
            {
                IsLogged();
            }
            else if (((Request["username"] == null) || (Request["firstname"] == null) || (Request["lastname"] == null) || (Request["gender"] == null) || (Request["DOB"] == null)) && (Session["r_username"] == null))
            {
                return Redirect("/Register");
            }
            else if ((Request["email"] == null) || (Request["pnumber"] == null) || (Request["password"] == null) || (Request["cpassword"] == null) || (Request["address"] == null))
            {
                return Redirect("Register/Index2");
            }
            else if ((Request["email"] == "") || (Request["pnumber"] == "") || (Request["password"] == "") || (Request["cpassword"] == "") || (Request["address"] == ""))
            {
                TempData["regerr_msg"] = "OOPS! Access Denied. Please fill up all blank boxes!";
                return Redirect("/Register/Index2");
            }
            else
            {
                if(Request["usertype"] != null)
                {
                    if (Request["usertype"].ToString().Equals("Admin"))
                    {
                        string username = Request["username"];
                        string firstname = Request["firstname"];
                        string lastname = Request["lastname"];
                        string gender = Request["gender"];
                        string DOB = Request["DOB"].ToString();

                        if (DB.CheckUsername(username) == false)
                        {
                            Session["r_username"] = username;
                            Session["r_firstname"] = firstname;
                            Session["r_lastname"] = lastname;
                            Session["r_gender"] = gender;
                            Session["r_DOB"] = DOB;
                            //return View();
                        }
                        else
                        {
                            TempData["adm_msg"] = "OOPS! This username already taken!";
                            return Redirect("/Admin");
                        }
                    }

                }

                Session["r_email"] = Request["email"];
                Session["r_pnumber"] = Request["pnumber"];
                Session["r_password"] = Request["password"];
                Session["r_address"] = Request["address"];
                Session["r_usertype"] = Request["usertype"];

                if (DB.CheckEmail(Request["email"]) == false)
                {
                    if (Request["password"].ToString() == Request["cpassword"].ToString())
                    {
                        //string hashpassword = CommonMethods.HashPassword(password);

                        if (DB.Register() == true)
                        {
                            if (Request["usertype"].ToString().Equals("Admin"))
                            {
                                TempData["adm_msg"] = "New Admin added Successfully! :)";
                                return Redirect("/Admin");
                            }
                            else
                            {
                                Session.Remove("username_err");
                                TempData["pwderr_msg"] = "Registration Successful! :) Please Login....";
                                return Redirect("/Login");
                            }                              
                        }
                        else
                        {
                            if (Request["usertype"].ToString().Equals("Admin"))
                            {
                                TempData["adm_msg"] = "New Admin assign Error! Something went wrong! Try Again.... :(";
                                return Redirect("/Admin");
                            }
                            else
                            {
                                //Session.Clear();
                                TempData["regerr_msg"] = "Registration Error! Something went wrong! Try Again.... :(";
                                return Redirect("/Register");
                            }
                        }
                    }
                    else
                    {
                        if (Request["usertype"].ToString().Equals("Admin"))
                        {
                            TempData["adm_msg"] = "OOPS! Error! Password did not match!";
                            return Redirect("/Admin");
                        }
                        else
                        {
                            TempData["regerr_msg"] = "OOPS! Error! Password did not match!";
                            return Redirect("/Register");
                        }
                    }
                }
                else
                {
                    if (Request["usertype"].ToString().Equals("Admin"))
                    {
                        TempData["adm_msg"] = "OOPS!  This email already taken!";
                        return Redirect("/Admin");
                    }
                    else
                    {
                        TempData["regerr_msg"] = "OOPS! This email already taken!";
                        return Redirect("/Register");
                    }
                }
            }
            //return View();
            return Redirect("/Register");
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