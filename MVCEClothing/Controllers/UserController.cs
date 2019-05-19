using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCEClothing.Models;

namespace MVCEClothing.Controllers
{
    public class UserController : Controller
    {
        AllDataTableContext DB = new AllDataTableContext();
        // GET: User
        public ActionResult Index()
        {
            DB.PageStats("userp_Count");
            /*if (DB.IsCookieEnabled() == false)
            {
                TempData["msg"] = "Your Cookie is Disabled!";
                return Redirect("/Index");
            }*/

            if (Session["usertype"] == null)
            {
                TempData["login_req"] = "Please Login 1st to View and Purchase Products";
                return Redirect("/Login");
            }
            else if ((String.Equals("admin", Session["usertype"].ToString())) || (String.Equals("Admin", Session["usertype"].ToString())))
            {
                return Redirect("/Admin");
            }
            else if ((String.Equals("user", Session["usertype"].ToString())) || (String.Equals("User", Session["usertype"].ToString())))
            {
                List<Product> products = DB.GetAllProducts();
                ViewBag.ProductFor = "All Clothing";
                return View(products);
            }
            else
            {
                return Redirect("/Index");
            }                 
        }

        public ActionResult MensClothing()
        {
            DB.PageStats("userp_Count");

            if (Session["usertype"] == null)
            {
                TempData["login_req"] = "Please Login 1st to View and Purchase Products";
                return Redirect("/Login");
            }
            else if ((String.Equals("admin", Session["usertype"].ToString())) || (String.Equals("Admin", Session["usertype"].ToString())))
            {
                TempData["login_req"] = "Please Login 1st to View and Purchase Products";
                return Redirect("/Admin");
            }
            else if ((String.Equals("user", Session["usertype"].ToString())) || (String.Equals("User", Session["usertype"].ToString())))
            {
                List<Product> products = DB.GetMenProducts();
                ViewBag.ProductFor = "Men's Clothing";
                return View("~/Views/User/index.cshtml", products);
            }
            else
            {
                return Redirect("/Index");
            }      
        }

        public ActionResult WomensClothing()
        {
            DB.PageStats("userp_Count");

            if (Session["usertype"] == null)
            {
                TempData["login_req"] = "Please Login 1st to View and Purchase Products";
                return Redirect("/Login");
            }
            else if ((String.Equals("admin", Session["usertype"].ToString())) || (String.Equals("Admin", Session["usertype"].ToString())))
            {
                return Redirect("/Admin");
            }
            else if ((String.Equals("user", Session["usertype"].ToString())) || (String.Equals("User", Session["usertype"].ToString())))
            {
                List<Product> products = DB.GetWomenProducts();
                ViewBag.ProductFor = "Women's Clothing";
                return View("~/Views/User/index.cshtml", products);
            }
            else
            {
                return Redirect("/Index");
            }      
        }

        public ActionResult ChildsClothing()
        {
            DB.PageStats("userp_Count");

            if (Session["usertype"] == null)
            {
                TempData["login_req"] = "Please Login 1st to View and Purchase Products";
                return Redirect("/Login");
            }
            else if ((String.Equals("admin", Session["usertype"].ToString())) || (String.Equals("Admin", Session["usertype"].ToString())))
            {
                return Redirect("/Admin");
            }
            else if ((String.Equals("user", Session["usertype"].ToString())) || (String.Equals("User", Session["usertype"].ToString())))
            {
                List<Product> products = DB.GetChildProducts();
                ViewBag.ProductFor = "Child's Clothing";
                return View("~/Views/User/index.cshtml", products);
            }
            else
            {
                return Redirect("/Index");
            }          
        }

        public ActionResult ClothDetails(int id)
        {
            if (Session["usertype"] == null)
            {
                TempData["login_req"] = "Please Login 1st to View and Purchase Products";
                return Redirect("/Login");
            }
            else if ((String.Equals("admin", Session["usertype"].ToString())) || (String.Equals("Admin", Session["usertype"].ToString())))
            {
                return Redirect("/Admin");
            }
            else if ((String.Equals("user", Session["usertype"].ToString())) || (String.Equals("User", Session["usertype"].ToString())))
            {
                Product product = DB.ProductDetails(id);
                return View(product);
            }
            else
            {
                return Redirect("/Index");
            }
        }
       
        [HttpPost]
        public ActionResult PurchaseProduct(int id)
        {
            if (Session["usertype"] == null)
            {
                TempData["login_req"] = "Please Login 1st to View and Purchase Products";
                return Redirect("/Login");
            }
            else if ((String.Equals("admin", Session["usertype"].ToString())) || (String.Equals("Admin", Session["usertype"].ToString())))
            {
                return Redirect("/Admin");
            }
            else if ((String.Equals("user", Session["usertype"].ToString())) || (String.Equals("User", Session["usertype"].ToString())))
            {
                Session["p_quantity"] = Request["quantity"];
                Session["p_size"] = Request["allsizes"];
                Session["p_pnumber"] = Request["pnumber"];
                Session["p_address"] = Request["address"];

                if (DB.AddPurchase(id) == true)
                {
                    TempData["usr_msg"] = "Product purchased successfull!.... :)";
                    return Redirect("/User/MyProfile");
                }
                else
                {
                    TempData["usr_msg"] = "Opps! Could not Purchase! Something went wrong! Try Again... :(";
                    return Redirect("/User");
                }
            }
            else
            {
                return Redirect("/Index");
            }
        }

        public ActionResult AddToCart() //List<Cartitems> products
        {
            var temp2 = Request["cartitems"];
            return Json(temp2, JsonRequestBehavior.AllowGet);
            /*if (Session["usertype"] == null)
            {
                TempData["login_req"] = "Please Login 1st to Check your cart list!";
                return Redirect("/Login");
            }
            else if ((String.Equals("admin", Session["usertype"].ToString())) || (String.Equals("Admin", Session["usertype"].ToString())))
            {
                return Redirect("/Admin");
            }
            else if ((String.Equals("user", Session["usertype"].ToString())) || (String.Equals("User", Session["usertype"].ToString())))
            {
                return View();
            }
            else
            {
                return Redirect("/Index");
            }*/
        }

        public ActionResult CartList()
        {
            DB.PageStats("cartlist_Count");

            if (Session["usertype"] == null)
            {
                TempData["login_req"] = "Please Login 1st to Check your cart list!";
                return Redirect("/Login");
            }
            else if ((String.Equals("admin", Session["usertype"].ToString())) || (String.Equals("Admin", Session["usertype"].ToString())))
            {
                return Redirect("/Admin");
            }
            else if ((String.Equals("user", Session["usertype"].ToString())) || (String.Equals("User", Session["usertype"].ToString())))
            {
                return View();
            }
            else
            {
                return Redirect("/Index");
            }          
        }

        public ActionResult MyProfile()
        {
            if (Session["usertype"] == null)
            {
                return Redirect("/Login");
            }
            else if ((String.Equals("user", Session["usertype"].ToString())) || (String.Equals("User", Session["usertype"].ToString())))
            {
                string username = Session["username"].ToString();
                List<Purchase> purchases = DB.GetUserPurchases(username);
                ViewBag.Purchases = purchases;
                return View();               
            }
            else if ((String.Equals("admin", Session["usertype"].ToString())) || (String.Equals("Admin", Session["usertype"].ToString())))
            {
                return Redirect("/User");
            }
            else
            {
                return Redirect("/Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateUser(int id)
        {
            if (Session["usertype"] == null)
            {
                return Redirect("/Login");
            }
            else if ((String.Equals("user", Session["usertype"].ToString())) || (String.Equals("User", Session["usertype"].ToString())))
            {
                if (Request["password"].ToString() == Request["cpassword"].ToString())
                {
                    Session["edit_username"] = Request["username"];
                    Session["edit_firstname"] = Request["firstname"];
                    Session["edit_lastname"] = Request["lastname"];
                    Session["edit_gender"] = Request["gender"];
                    //Session["edit_DOB"] = Request["DOB"].ToString();
                    Session["edit_email"] = Request["email"];
                    Session["edit_pnumber"] = Request["pnumber"];
                    Session["edit_password"] = Request["password"];
                    Session["edit_address"] = Request["address"];
                    Session["edit_usertype"] = Request["usertype"];

                    if (DB.UpdateUser(id) == true)
                    {
                        TempData["usr_msg"] = "Profile Updated Successfully! :)";
                        return Redirect("/Admin");
                    }
                    else
                    {
                        TempData["usr_msg"] = "Opps! Coud not update Profile! Something went wrong! Try Again.... :(";
                        return Redirect("/Admin");
                    }
                }
                else if (Request["cpassword"].ToString() == "")
                {
                    TempData["usr_msg"] = "Please confirm your password! Then Try click update Info....";
                    return Redirect("/Admin");

                }
                else
                {
                    TempData["usr_msg"] = "Password did not match! Try Again.... :(";
                    return Redirect("/Admin");
                }

            }
            else if ((String.Equals("admin", Session["usertype"].ToString())) || (String.Equals("Admin", Session["usertype"].ToString())))
            {
                return Redirect("/Admin");
            }
            else
            {
                return Redirect("/Index");
            }
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