using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCEClothing.Models;

namespace MVCEClothing.Controllers
{
    public class IndexController : Controller
    {
        AllDataTableContext DB = new AllDataTableContext();
        // GET: Index
        public ActionResult Index()
        {
            //DB.IsCookieEnabled();

            if ((Request.Cookies["userInfo"] != null) && (Request.Cookies["userInfo"]["username"] != "") && (Request.Cookies["userInfo"]["hashpassword"] != ""))
            {
                DB.CheckCookie();
            }

            return View();
        }
    }
}