using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cellphone { get; set; }
        public bool Gender { get; set; }
        public string BillAddress { get; set; }
        public string ShipAddress { get; set; }
        public string Email { get; set; }
        public int Credit { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastMod { get; set; }
    }
}