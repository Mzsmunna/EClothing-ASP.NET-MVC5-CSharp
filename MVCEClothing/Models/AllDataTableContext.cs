using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

//By Mzs
using System.Text;
using System.Security.Cryptography;
using System.Web.Mvc;

namespace MVCEClothing.Models
{
    public class AllDataTableContext : DbContext
    {
        public AllDataTableContext() : base("name=EClothingConnection")
        {
            
        }

        public DbSet<User> UserTable { set; get; }
        public DbSet<Product> ProductTable { set; get; }
        public DbSet<Purchase> PurchaseTable { set; get; }

        //-------------------         Start of All UserTable Methods & Functionality        -------------------- //

        public bool GetUser(string username) // UserTable Methods
        {
            var user = from allusers in UserTable
                       where allusers.Username.Equals(username)
                       || allusers.Email.Equals(username)
                       select allusers;

            if (user.Count() >= 1)
            {
                var xuser = user.Single();

                HttpContext.Current.Session["username"] = xuser.Username;
                HttpContext.Current.Session["email"] = xuser.Email;
                return true;
            }
            else
            {
                return false;
            }
            //return (User)user;
        }

        public List<User> GetAllUsers() // UserTable Methods
        {
            var allusers = from users in UserTable select users;

            List<User> usersList = new List<User>();

            foreach (var user in allusers)
            {
                usersList.Add(user);
            }

            return usersList;
        }

        public bool CheckUsername(string username) // UserTable Methods
        {
            var user = from allusers in UserTable
                       where allusers.Username.Equals(username)
                       select allusers;
            return (user.Count() >= 1) ? true : false;
        }

        public bool CheckEmail(string email) // UserTable Methods
        {
            var user = from allusers in UserTable
                       where allusers.Email.Equals(email)
                       select allusers;
            return (user.Count() >= 1) ? true : false;
        }

        public bool Register() // UserTable Methods
        {
            try
            {
                User newuser = new User();
                newuser.Username = HttpContext.Current.Session["r_username"].ToString();
                newuser.Firstname = HttpContext.Current.Session["r_firstname"].ToString();
                newuser.Lastname = HttpContext.Current.Session["r_lastname"].ToString();
                newuser.Gender = HttpContext.Current.Session["r_gender"].ToString();
                newuser.Email = HttpContext.Current.Session["r_email"].ToString();
                newuser.Phonenumber = HttpContext.Current.Session["r_pnumber"].ToString();
                newuser.Password = HttpContext.Current.Session["r_password"].ToString();
                newuser.address = HttpContext.Current.Session["r_address"].ToString();
                newuser.Usertype = HttpContext.Current.Session["r_usertype"].ToString();
                UserTable.Add(newuser);
                SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool Login(string username, string password) //UserTable Methods
        {
            var user = from allusers in UserTable
                       where allusers.Username.Equals(username)
                       && allusers.Password.Equals(password)
                       select allusers;
            if (user.Count() >= 1)
            {
                var xuser = user.Single();

                if (xuser.Password.Equals(password))
                {
                    HttpContext.Current.Session["uid"] = xuser.Id;
                    HttpContext.Current.Session["username"] = xuser.Username;
                    HttpContext.Current.Session["firstname"] = xuser.Firstname;
                    HttpContext.Current.Session["lastname"] = xuser.Lastname;
                    HttpContext.Current.Session["gender"] = xuser.Gender;
                    HttpContext.Current.Session["email"] = xuser.Email;
                    HttpContext.Current.Session["pnumber"] = xuser.Phonenumber;
                    HttpContext.Current.Session["address"] = xuser.address;
                    HttpContext.Current.Session["password"] = xuser.Password;
                    HttpContext.Current.Session["usertype"] = xuser.Usertype;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            //return (User)user;
        }

        public bool UpdateUser(int id) // UserTable Methods
        {
            try
            {
                var edituser = (from allusers in UserTable
                                where allusers.Id == id
                                select allusers).First();

                edituser.Username = HttpContext.Current.Session["edit_username"].ToString();
                edituser.Firstname = HttpContext.Current.Session["edit_firstname"].ToString();
                edituser.Lastname = HttpContext.Current.Session["edit_lastname"].ToString();
                edituser.Gender = HttpContext.Current.Session["edit_gender"].ToString();
                edituser.Email = HttpContext.Current.Session["edit_email"].ToString();
                edituser.Phonenumber = HttpContext.Current.Session["edit_pnumber"].ToString();
                edituser.Password = HttpContext.Current.Session["edit_password"].ToString();
                edituser.address = HttpContext.Current.Session["edit_address"].ToString();
                edituser.Usertype = HttpContext.Current.Session["edit_usertype"].ToString();
                SaveChanges();

                HttpContext.Current.Session["username"] = edituser.Username;
                HttpContext.Current.Session["firstname"] = edituser.Firstname;
                HttpContext.Current.Session["lastname"] = edituser.Lastname;
                HttpContext.Current.Session["gender"] = edituser.Gender;
                HttpContext.Current.Session["email"] = edituser.Email;
                HttpContext.Current.Session["pnumber"] = edituser.Phonenumber;
                HttpContext.Current.Session["password"] = edituser.Password;
                HttpContext.Current.Session["address"] = edituser.address;
                HttpContext.Current.Session["usertype"] = edituser.Usertype;

                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool FindUser(string usernamef, string emailf) // UserTable Methods
        {
            try
            {
                var user = (from users in UserTable
                            where users.Username.Equals(usernamef)
                            && users.Email.Equals(emailf)
                            select users).First();

                if (user != null)
                {
                    HttpContext.Current.Session["usernamef"] = usernamef;
                    HttpContext.Current.Session["emailf"] = emailf;
                    HttpContext.Current.Session["uidf"] = user.Id;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool ResetPassword(string passwordf) // UserTable Methods
        {
            try
            {
                int id = Convert.ToInt32(HttpContext.Current.Session["uidf"].ToString());
                var user = (from users in UserTable
                            where users.Id == id
                            select users).First();

                if (user != null)
                {
                    user.Password = passwordf;
                    SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        //---------------------X         End of All UserTable Methods & Functionality        X-------------------- //



        //---------------------       Start of All ProductTable Methods & Functionality        -------------------- //

        public bool GetProduct(int id) // ProductTable Methods
        {
            var product = from allproducts in ProductTable
                       where allproducts.Pid == id
                       select allproducts;

            if (product.Count() >= 1)
            {
                var xproduct = product.Single();

                HttpContext.Current.Session["edit_pid"] = xproduct.Pid;
                HttpContext.Current.Session["edit_pname"] = xproduct.Itemname;
                HttpContext.Current.Session["edit_pfor"] = xproduct.Itemfor;
                HttpContext.Current.Session["edit_category"] = xproduct.Category;
                HttpContext.Current.Session["edit_sizes"] = xproduct.Sizes;
                HttpContext.Current.Session["edit_available"] = xproduct.Available;
                HttpContext.Current.Session["edit_price"] = xproduct.Price;
                HttpContext.Current.Session["edit_currency"] = xproduct.Currency;
                HttpContext.Current.Session["edit_cost"] = xproduct.Cost;
                HttpContext.Current.Session["edit_offer"] = xproduct.Offer;
                return true;
            }
            else
            {
                return false;
            }
            //return (Product)product;
        }

        public List<Product> GetAllProducts() // ProductTable Methods
        {
            var allproducts = from products in ProductTable select products;

            List<Product> productsList = new List<Product>();

            foreach (var product in allproducts)
            {
                productsList.Add(product);
            }

            return productsList;
        }

        public List<Product> GetMenProducts() // ProductTable Methods
        {
            var allproducts = from products in ProductTable where products.Itemfor == "Men" select products;

            List<Product> menProductlist = new List<Product>();

            foreach (var product in allproducts)
            {
                menProductlist.Add(product);
            }

            return menProductlist;
        }

        public List<Product> GetWomenProducts() // ProductTable Methods
        {
            var allproducts = from products in ProductTable where products.Itemfor == "Women" select products;

            List<Product> womenProductlist = new List<Product>();

            foreach (var product in allproducts)
            {
                womenProductlist.Add(product);
            }

            return womenProductlist;
        }

        public List<Product> GetChildProducts() // ProductTable Methods
        {
            var allproducts = from products in ProductTable where products.Itemfor == "Child" select products;

            List<Product> childProductlist = new List<Product>();

            foreach (var product in allproducts)
            {
                childProductlist.Add(product);
            }

            return childProductlist;
        }

        public Product ProductDetails(int id) // ProductTable Methods
        {
            var product = (from allproducts in ProductTable
                          where allproducts.Pid == id
                          select allproducts).First();

            return product;
        }

        public bool AddProduct() // ProductTable Methods
        {
            try
            {
                Product newproduct = new Product();
                newproduct.Itemname = HttpContext.Current.Session["add_pname"].ToString();
                newproduct.Itemfor = HttpContext.Current.Session["add_pfor"].ToString();
                newproduct.Category = HttpContext.Current.Session["add_category"].ToString();
                newproduct.Sizes = HttpContext.Current.Session["add_sizes"].ToString();
                newproduct.Available = Convert.ToInt32(HttpContext.Current.Session["add_available"].ToString());
                newproduct.Price = Convert.ToInt32(HttpContext.Current.Session["add_price"].ToString());
                newproduct.Currency = HttpContext.Current.Session["add_currency"].ToString();
                newproduct.Cost = Convert.ToInt32(HttpContext.Current.Session["add_cost"].ToString());
                newproduct.Offer = Convert.ToInt32(HttpContext.Current.Session["add_offer"].ToString());
                ProductTable.Add(newproduct);
                SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }           
        }

        public bool UpdateProduct(int id) // ProductTable Methods
        {
            try
            {
                var editproduct = (from allproducts in ProductTable
                                   where allproducts.Pid == id
                                   select allproducts).First();

                editproduct.Itemname = HttpContext.Current.Session["edit_pname"].ToString();
                editproduct.Itemfor = HttpContext.Current.Session["edit_pfor"].ToString();
                editproduct.Category = HttpContext.Current.Session["edit_category"].ToString();
                editproduct.Sizes = HttpContext.Current.Session["edit_sizes"].ToString();
                editproduct.Available = Convert.ToInt32(HttpContext.Current.Session["edit_available"].ToString());
                editproduct.Price = Convert.ToInt32(HttpContext.Current.Session["edit_price"].ToString());
                editproduct.Currency = HttpContext.Current.Session["edit_currency"].ToString();
                editproduct.Cost = Convert.ToInt32(HttpContext.Current.Session["edit_cost"].ToString());
                editproduct.Offer = Convert.ToInt32(HttpContext.Current.Session["edit_offer"].ToString());
                SaveChanges();

                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool DeleteProduct(int id) // ProductTable Methods
        {
            try
            {
                var deleteproduct = (from allproducts in ProductTable
                                     where allproducts.Pid == id
                                     select allproducts).First();

                if (deleteproduct != null)
                {
                    ProductTable.Remove(deleteproduct);
                    SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        //-------------------X      End of All ProductTable Methods & Functionality        X----------------------//




        //-------------------       Start of All PurchaseTable Methods & Functionality        ----------------------//

        public List<Purchase> GetAllPurchases() // PurchaseTable Methods
        {
            var allpurchases = from purchases in PurchaseTable select purchases;

            List<Purchase> purchasesList = new List<Purchase>();

            foreach (var purchase in allpurchases)
            {
                purchasesList.Add(purchase);
            }

            return purchasesList;
        }

        public List<Purchase> GetUserPurchases(string username) // PurchaseTable Methods
        {
            var allpurchases = from purchases in PurchaseTable
                               where purchases.Purchasedby.Equals(username)
                               select purchases;

            List<Purchase> purchasesList = new List<Purchase>();
            if(allpurchases.Count() >= 1 )
            {
                foreach (var purchase in allpurchases)
                {
                    purchasesList.Add(purchase);
                }

                return purchasesList;
            }
            else
            {
                return null;
            }
            
            //return purchasesList;
        }

        public bool AddPurchase(int id) // PurchaseTable Methods
        {
            try
            {
                var product = (from allproducts in ProductTable
                               where allproducts.Pid == id
                               select allproducts).First();
                int quantity = Convert.ToInt32(HttpContext.Current.Session["p_quantity"].ToString());
                int available = product.Available - quantity;
                product.Available = available;
                SaveChanges();

                Purchase newpurchase = new Purchase();

                newpurchase.Pid = product.Pid;
                newpurchase.Itemname = product.Itemname;
                newpurchase.Itemfor = product.Itemfor;
                newpurchase.Category = product.Category;
                newpurchase.Sizes = HttpContext.Current.Session["p_size"].ToString();
                newpurchase.Quantity = quantity;
                newpurchase.Price = product.Price;
                newpurchase.Currency = product.Currency;
                newpurchase.Cost = product.Cost;
                newpurchase.TotalPrice = product.Price * quantity;
                newpurchase.Offer = product.Offer;
                newpurchase.Phonenumber = HttpContext.Current.Session["p_pnumber"].ToString();
                newpurchase.Purchasedby = HttpContext.Current.Session["username"].ToString();
                newpurchase.address = HttpContext.Current.Session["p_address"].ToString();
                newpurchase.PurchaseDate = DateTime.Now;
                PurchaseTable.Add(newpurchase);
                SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                //HttpContext.Current.Session["ex"] = ex.ToString();
                Console.WriteLine(ex);
                return false;
            }
        }
        //-------------------X        End Of All ProductTable Methods & Functionality        X-------------------- //




        //---------------  START OF NON DB RELATED BUT DATA MANUPULATION METHODS & FUNCTIONALITIES  ---------------//
        public static string HashPassword(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public void SetCookie(string username, string hashpassword)
        {
            HttpContext.Current.Response.Cookies["userInfo"]["username"] = username;
            HttpContext.Current.Response.Cookies["userInfo"]["hashpassword"] = hashpassword;
            HttpContext.Current.Response.Cookies["userInfo"]["lastVisit"] = DateTime.Now.ToString();
            HttpContext.Current.Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(7);

            HttpCookie aCookie = new HttpCookie("userInfo");
            aCookie.Values["username"] = username;
            aCookie.Values["hashpassword"] = hashpassword;
            aCookie.Values["lastVisit"] = DateTime.Now.ToString();
            aCookie.Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Response.Cookies.Add(aCookie);
        }

        public void CheckCookie()
        {
            string cookie_username = HttpContext.Current.Server.HtmlEncode(HttpContext.Current.Request.Cookies["userInfo"]["username"]);
            string cookie_password = HttpContext.Current.Server.HtmlEncode(HttpContext.Current.Request.Cookies["userInfo"]["hashpassword"]);

            if (Login(cookie_username, cookie_password) == true)
            {
                PageStats("total_logged");

                HttpContext.Current.Session["remember"] = "remembered-ok";

                if ((HttpContext.Current.Session["usertype"].ToString() == "admin") || (HttpContext.Current.Session["usertype"].ToString() == "Admin"))
                {
                    HttpContext.Current.Response.Redirect("~/Admin");
                }
                else if ((HttpContext.Current.Session["usertype"].ToString() == "user") || (HttpContext.Current.Session["usertype"].ToString() == "User"))
                {
                    HttpContext.Current.Response.Redirect("~/User");
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/Login");
            }
        }

        public bool IsCookieEnabled()
        {
            if (HttpContext.Current.Request.Browser.Cookies)
            {
                //Your Browser supports cookies 
                if (HttpContext.Current.Request.QueryString["TestingCookie"] == null)
                {
                    //not testing the cookie so create it
                    HttpCookie cookie = new HttpCookie("CookieTest", "");
                    HttpContext.Current.Response.Cookies.Add(cookie);

                    //redirect to same page because the cookie will be written to the client computer, 
                    //only upon sending the response back from the server 
                    //HttpContext.Current.Response.Redirect(path + "?TestingCookie=1");
                    return true;
                }
                else
                {
                    //let's check if Cookies are enabled
                    if (HttpContext.Current.Request.Cookies["CookieTest"] == null)
                    {
                        //Cookies are disabled
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Your Cookie is Disabled!')", true);
                        //HttpContext.Current.Response.Write("<h4>Your Cookie is Disabled!!<h4>");
                        //HttpContext.Current.Response.Redirect("~/Index");
                        return false;
                    }
                    else
                    {
                        //Cookies are enabled
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Your Cookie is Enabled!')", true);
                        //Response.Write("Your Cookie is Enabled!");
                        return true;
                    }
                }

            }
            else
            {
                // Your Browser does not support cookies
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Your Browser does not support cookies!')", true);
                //HttpContext.Current.Response.Write("Your Browser does not support cookies");
                return false;
            }
        }

        public void PageStats(string keyName)
        {
            if (HttpContext.Current.Application[keyName] == null)
            {
                HttpContext.Current.Application[keyName] = 1;
            }
            else
            {
                int count = Convert.ToInt32(HttpContext.Current.Application[keyName]);
                HttpContext.Current.Application[keyName] = ++count;
            }

            if (HttpContext.Current.Session[keyName] == null)
            {
                HttpContext.Current.Session[keyName] = 1;
            }
            else
            {
                int count = Convert.ToInt32(HttpContext.Current.Session[keyName]);
                HttpContext.Current.Session[keyName] = ++count;
            }
        }
        //-----------X      END OF NON DB RELATED BUT DATA MANUPULATION METHODS & FUNCTIONALITIES          X-------//



        //-----------------    START OF NON USED & BASICALLY For Dummy Data Creation Purpose  ---------------------//
        public List<User> GetUsers(AllDataTableContext context)
        {
            List<User> Users = new List<User>()
            {
                new User
                {
                    //Id = ,
                    Username = "",
                    Firstname = "",
                    Lastname = "",
                    Gender = "",
                    Email = "",
                    Phonenumber = "",
                    address = "",
                    Password = "",
                    Usertype = ""
                }
            };

            return Users;
        }

        public List<Product> GetProducts(AllDataTableContext context)
        {
            List<Product> Products = new List<Product>()
            {
                new Product
                {
                    //Id = ,
                    //Pid = ,
                    Itemname = "",
                    Category = "",
                    Itemfor = "",
                    Sizes = "",
                    Available = 0,
                    Price = 0,
                    Currency = "",
                    Cost = 0,
                    Offer = 0
                }
            };

            return Products;
        }

        public List<Purchase> GetPurchases(AllDataTableContext context)
        {
            List<Purchase> Purchases = new List<Purchase>()
            {
                new Purchase
                {
                    //Id = ,
                    //Prid = ,
                    //Pid = context.ProductTable.Find(0).Pid, //foreign key convert issue
                    Itemname = "",
                    Category = "",
                    Itemfor = "",
                    Sizes = "",
                    Quantity = 0,
                    Price = 0,
                    TotalPrice = 0,
                    Currency = "",
                    Cost = 0,
                    Offer = 0,
                    Purchasedby = "",
                    Phonenumber = "",
                    address = "",
                    //PurchaseDate = "time"
                }
            };

            return Purchases;
        }
        //----------X            END OF NON USED & BASICALLY For Dummy Data Creation Purpose          X------------//
    }
}