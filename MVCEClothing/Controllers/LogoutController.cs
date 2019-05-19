using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCEClothing.Controllers
{
    public class LogoutController : Controller
    {
        // GET: Logout
        public ActionResult Index()
        {
            //Session.Remove("YourItem");
            //Session.Clear();
            if (System.Web.HttpContext.Current.Application["total_logged"] != null)
            {
                int total_logged = Convert.ToInt32(System.Web.HttpContext.Current.Application["total_logged"]);
                System.Web.HttpContext.Current.Application["total_logged"] = --total_logged;

                if (total_logged <= 0)
                {
                    System.Web.HttpContext.Current.Application["total_logged"] = 0;
                }
            }

            Session.Abandon();

            if (System.Web.HttpContext.Current.Request.Cookies["userInfo"] != null)
            {
                SetCookie("", "");
            }

            return Redirect("/Login");
        }

        private void SetCookie(string username, string hashpassword)
        {
            System.Web.HttpContext.Current.Response.Cookies["userInfo"]["username"] = username;
            System.Web.HttpContext.Current.Response.Cookies["userInfo"]["hashpassword"] = hashpassword;
            System.Web.HttpContext.Current.Response.Cookies["userInfo"]["lastVisit"] = DateTime.Now.ToString();
            System.Web.HttpContext.Current.Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(7);

            HttpCookie aCookie = new HttpCookie("userInfo");
            aCookie.Values["username"] = username;
            aCookie.Values["hashpassword"] = hashpassword;
            aCookie.Values["lastVisit"] = DateTime.Now.ToString();
            aCookie.Expires = DateTime.Now.AddDays(1);
            System.Web.HttpContext.Current.Response.Cookies.Add(aCookie);
        }
    }
}