using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCEClothing.Models;

namespace MVCEClothing.Controllers
{
    public class ForgetController : Controller
    {
        AllDataTableContext DB = new AllDataTableContext();
        // GET: Forget
        public ActionResult Index()
        {
            DB.PageStats("frgt_Count");

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
        public ActionResult Reset()
        {
            //CommonMethods my_methods = new CommonMethods();

            if (Session["usertype"] != null)
            {
                IsLogged();
            }
            else if ((Session["usernamef"] != null) && (Session["emailf"] != null) && (Request["usernamef"] == null) && (Request["emailf"] == null))
            {
                return View();
            }
            else if ((Request["usernamef"] == null) || (Request["emailf"] == null))
            {
                return Redirect("/Forget");
            }
            else if ((Request["usernamef"] == "") || (Request["emailf"] == ""))
            {
                TempData["frgt_err"] = "Access Denied. Please Enter your username and email both!";
                return Redirect("/Forget");
            }
            else
            {
                string usernamef = Request["usernamef"];
                string emailf = Request["emailf"];

                if (DB.FindUser(usernamef, emailf) == true)
                {
                    return View();
                }
                else
                {
                    TempData["frgt_err"] = "We did not find any user with same username and email!!";
                    return Redirect("/Forget");
                }
            }

            return Redirect("/Forget");
            //return View();
        }

        [HttpPost]
        public ActionResult Confirm()
        {
            if (Session["usertype"] != null)
            {
                IsLogged();
            }
            else if ((Request["passwordf"] == null) || (Request["cpasswordf"] == null))
            {
                return Redirect("/Forget");
            }
            else if ((Request["passwordf"] == "") || (Request["cpasswordf"] == ""))
            {
                TempData["frgt_err"] = "Access Denied. Please Enter your Password in both field!";
                return Redirect("/Forget");
            }
            else
            {
                string passwordf = Request["passwordf"];
                string cpasswordf = Request["cpasswordf"];

                if(passwordf.Equals(cpasswordf))
                {
                    if (DB.ResetPassword(passwordf) == true)
                    {
                        Session.Remove("uidf");
                        Session.Remove("usernamef");
                        Session.Remove("emailf");
                        Session.Remove("loginblock");
                        Session.Remove("timeduration");
                        Session.Remove("password_err");
                        TempData["pwderr_msg"] = "Password Updated and Account retrieved successfully! Now you can login";
                        return Redirect("/Login");
                    }
                    else
                    {
                        TempData["frgt_err"] = "OPPS! Something went wrong! Unable to reset password and Retrieve your account!! Try again .... :(";
                        return Redirect("/Forget");
                    }

                }
                else
                {
                    TempData["frgt_err"] = "Password Did not Match!! Try again .... :(";
                    return Redirect("/Forget/Reset");
                }
                
            }
            //return View();
            return Redirect("/Login");
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