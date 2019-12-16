using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// ReSharper disable InconsistentNaming

namespace ASP.NET_Project.Models
{
    public class CartItem
    {
        public int id { get; set; }
        public int count { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public double total { get; set; }
    }
}