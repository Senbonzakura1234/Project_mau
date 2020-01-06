// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASP.NET_Project.Models
{
    public class OrderItem
    {
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        [Key, Column(Order = 0)]
        public Guid ProductId { get; set; }
        [Key, Column(Order = 1)]
        public int OrderId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}