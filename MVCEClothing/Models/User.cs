using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCEClothing.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { set; get; }
        public string Username { set; get; }
        public string Firstname { set; get; }
        public string Lastname { set; get; }
        public string Gender { set; get; }
        public string Email { set; get; }
        public string Phonenumber { set; get; }
        public string address { set; get; }
        public string Password { set; get; }
        public string Usertype { set; get; }
    }
}