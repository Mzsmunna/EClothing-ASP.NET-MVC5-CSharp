using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCEClothing.Models;

namespace MVCEClothing.Controllers
{
    public class AdminController : Controller
    {
        AllDataTableContext DB = new AllDataTableContext();
        // GET: Admin
        public ActionResult Index()
        {         
            DB.PageStats("adminp_Count");

            if (Session["usertype"] == null)
            {
                return Redirect("/Login");
            }
            else if ((String.Equals("user", Session["usertype"].ToString())) || (String.Equals("User", Session["usertype"].ToString())))
            {
                return Redirect("/User");
            }
            else if ((String.Equals("admin", Session["usertype"].ToString())) || (String.Equals("Admin", Session["usertype"].ToString())))
            {
                List<Product> products = DB.GetAllProducts();
                List<User> users = DB.GetAllUsers();
                List<Purchase> purchases = DB.GetAllPurchases();

                ViewBag.Users = users;
                ViewBag.Purchases = purchases;
                //ViewData["allproducts"] = Products;

                return View(products);
            }
            else
            {
                return Redirect("/Index");
            }
            
        }

        public ActionResult AddProduct()
        {
            if (Session["usertype"] == null)
            {
                return Redirect("/Login");
            }
            else if ((String.Equals("user", Session["usertype"].ToString())) || (String.Equals("User", Session["usertype"].ToString())))
            {
                return Redirect("/User");
            }
            else if ((String.Equals("admin", Session["usertype"].ToString())) || (String.Equals("Admin", Session["usertype"].ToString())))
            {
                ViewBag.Action = "Add";
                return View();
            }
            else
            {
                return Redirect("/Index");
            }
        }

        [HttpPost]
        public ActionResult ProductAdded()
        {
            if (Session["usertype"] == null)
            {
                return Redirect("/Login");
            }
            else if ((String.Equals("user", Session["usertype"].ToString())) || (String.Equals("User", Session["usertype"].ToString())))
            {
                return Redirect("/User");
            }
            else if ((String.Equals("admin", Session["usertype"].ToString())) || (String.Equals("Admin", Session["usertype"].ToString())))
            {             
                string psizes = "";
                if (Request["XS"] != null)
                {
                    Session["XS"] = Request["XS"];
                    psizes += Request["XS"] + ", ";
                }
                if (Request["S"] != null)
                {
                    Session["S"] = Request["S"];
                    psizes += Request["S"] + ", ";
                }
                if (Request["M"] != null)
                {
                    Session["M"] = Request["M"];
                    psizes += Request["M"] + ", ";
                }
                if (Request["L"] != null)
                {
                    Session["L"] = Request["L"];
                    psizes += Request["L"] + ", ";
                }
                if (Request["XL"] != null)
                {
                    Session["XL"] = Request["XL"];
                    psizes += Request["XL"] + ", ";
                }
                if (Request["XXL"] != null)
                {
                    Session["XXL"] = Request["XXL"];
                    psizes += Request["XXL"] + ", ";
                }

                Session["add_pname"] = Request["pname"];
                Session["add_pfor"] = Request["pfor"];
                Session["add_category"] = Request["category"];
                Session["add_sizes"] = psizes;
                Session["add_available"] = Request["available"];
                Session["add_price"] = Request["price"];
                Session["add_currency"] = Request["currency"];
                Session["add_cost"] = Request["cost"];
                Session["add_offer"] = Request["offer"];
                Session["actiontype"] = Request["actiontype"];
                
                if (DB.AddProduct() == true)
                {
                    TempData["adm_msg"] = "Product Added Successfully! :)";
                }
                else
                {
                    TempData["adm_msg"] = "Unable to Add! Something went wrong! Try Again....:(";
                }

                return Redirect("/Admin");
            }
            else
            {
                return Redirect("/Index");
            }
        }

        [HttpPost]
        public ActionResult ProductUpdate(int id)
        {
            if (Session["usertype"] == null)
            {
                return Redirect("/Login");
            }
            else if ((String.Equals("user", Session["usertype"].ToString())) || (String.Equals("User", Session["usertype"].ToString())))
            {
                return Redirect("/User");
            }
            else if ((String.Equals("admin", Session["usertype"].ToString())) || (String.Equals("Admin", Session["usertype"].ToString())))
            {
                string psizes = "";
                if (Request["XS"] != null)
                {
                    Session["XS"] = Request["XS"];
                    psizes += Request["XS"] + ", ";
                }
                if (Request["S"] != null)
                {
                    Session["S"] = Request["S"];
                    psizes += Request["S"] + ", ";
                }
                if (Request["M"] != null)
                {
                    Session["M"] = Request["M"];
                    psizes += Request["M"] + ", ";
                }
                if (Request["L"] != null)
                {
                    Session["L"] = Request["L"];
                    psizes += Request["L"] + ", ";
                }
                if (Request["XL"] != null)
                {
                    Session["XL"] = Request["XL"];
                    psizes += Request["XL"] + ", ";
                }
                if (Request["XXL"] != null)
                {
                    Session["XXL"] = Request["XXL"];
                    psizes += Request["XXL"] + ", ";
                }

                Session["edit_pname"] = Request["pname"];
                Session["edit_pfor"] = Request["pfor"];
                Session["edit_category"] = Request["category"];
                Session["edit_sizes"] = psizes;
                Session["edit_available"] = Request["available"];
                Session["edit_price"] = Request["price"];
                Session["edit_currency"] = Request["currency"];
                Session["edit_cost"] = Request["cost"];
                Session["edit_offer"] = Request["offer"];             

                if (DB.UpdateProduct(id) == true)
                {
                    TempData["adm_msg"] = "Product Edited Successfully! :)";
                }
                else
                {
                    TempData["adm_msg"] = "Unable to Update! Something went wrong! Try Again.... :(";
                }
                return Redirect("/Admin");
            }
            else
            {
                return Redirect("/Index");
            }
        }

        public ActionResult EditProduct(int id)
        {
            if (Session["usertype"] == null)
            {
                return Redirect("/Login");
            }
            else if ((String.Equals("user", Session["usertype"].ToString())) || (String.Equals("User", Session["usertype"].ToString())))
            {
                return Redirect("/User");
            }
            else if ((String.Equals("admin", Session["usertype"].ToString())) || (String.Equals("Admin", Session["usertype"].ToString())))
            {
                ViewBag.PID = id;
                if (DB.GetProduct(id) == true)
                {
                    return View();
                }
                else
                {
                    TempData["adm_msg"] = "Opps! Something went wrong! Try Again....";
                    return Redirect("/Admin");
                }
                //return View("~/Views/Admin/AddProduct.cshtml");
            }
            else
            {
                return Redirect("/Index");
            }          
        }

        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            if (Session["usertype"] == null)
            {
                return Redirect("/Login");
            }
            else if ((String.Equals("user", Session["usertype"].ToString())) || (String.Equals("User", Session["usertype"].ToString())))
            {
                return Redirect("/User");
            }
            else if ((String.Equals("admin", Session["usertype"].ToString())) || (String.Equals("Admin", Session["usertype"].ToString())))
            {
                ViewBag.PID = id;
                if(Request["Yes"] != null)
                {
                    if (DB.DeleteProduct(id) == true)
                    {
                        TempData["adm_msg"] = "Product has been deleted successfully.... :)";
                        return Redirect("/Admin");
                    }
                    else
                    {
                        TempData["adm_msg"] = "Opps! Couldn't delete! Something went wrong! Try Again... :(";
                        return Redirect("/Admin");
                    }
                }
                else
                {
                    return Redirect("/Admin");
                }
            }
            else
            {
                return Redirect("/Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateAdmin(int id)
        {
            if (Session["usertype"] == null)
            {
                return Redirect("/Login");
            }
            else if ((String.Equals("user", Session["usertype"].ToString())) || (String.Equals("User", Session["usertype"].ToString())))
            {
                return Redirect("/User");
            }
            else if ((String.Equals("admin", Session["usertype"].ToString())) || (String.Equals("Admin", Session["usertype"].ToString())))
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
                        TempData["adm_msg"] = "Profile Updated Successfully! :)";
                        return Redirect("/Admin");
                    }
                    else
                    {
                        TempData["adm_msg"] = "Opps! Coud not update Profile! Something went wrong! Try Again.... :(";
                        return Redirect("/Admin");
                    }
                }
                else if (Request["cpassword"].ToString() == "")
                {
                    TempData["adm_msg"] = "Please confirm your password! Then Try click update Info....";
                    return Redirect("/Admin");

                }
                else
                {
                    TempData["adm_msg"] = "Password did not match! Try Again.... :(";
                    return Redirect("/Admin");
                }
            }
            else
            {
                return Redirect("/Index");
            }
        }
    }
}