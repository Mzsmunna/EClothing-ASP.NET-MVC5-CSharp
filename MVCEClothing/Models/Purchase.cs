using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCEClothing.Models
{
    [Table("Purchase")]
    public class Purchase
    {
        [Key]
        public int Prid { set; get; }
        public int Pid { set; get; }
        public string Itemname { set; get; }
        public string Category { set; get; }
        public string Itemfor { set; get; }
        public string Sizes { set; get; }
        public int Quantity { set; get; }
        public float Price { set; get; }
        public float TotalPrice { set; get; }
        public string Currency { set; get; }
        public float Cost { set; get; }
        public int Offer { set; get; }
        public string Purchasedby { set; get; }
        public string Phonenumber { set; get; }  
        public string address { set; get; }
        public DateTime PurchaseDate { get; set; }
    }
}