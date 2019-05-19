using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCEClothing.Models;

namespace MVCEClothing.Controllers
{
    public class TestController : Controller
    {
        AllDataTableContext DB = new AllDataTableContext();

        // GET: Test
        public ActionResult Index()
        {
            var users = from allusers in DB.UserTable select allusers;

            List<User> Users = new List<User>();

            foreach (var user in users)
            {
                Users.Add(user);
            }
            //ViewData["allusers"] = Users;
            DB.UserTable.ToList();
            DB.ProductTable.ToList();
            DB.PurchaseTable.ToList();
            return View(Users);
        }

        public ActionResult AddToCart(List<Cartitems> products)
        {
            var temp = products;
            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        public class Cartitems
        {
            public int pid { get; set; }
            public string pname { get; set; }
            public string pfor { get; set; }
            public string category { get; set; }
            public int quantity { get; set; }
            public string quantityFor { get; set; }
            public float price { get; set; }
            public float cost { get; set; }
            public float total { get; set; }
            public string currency { get; set; }
            public int offer { get; set; }
            public int available { get; set; }

        }
    }
}