// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASP.NET_Project.Models;
// ReSharper disable CommentTypo

namespace ASP.NET_Project.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly MyDbContext _db = new MyDbContext();

        // ReSharper disable once UnusedMember.Local


        // GET: ShoppingCart
        public ActionResult Index()
        {
            return null;
        }


        public ActionResult CreateOrder(List<CartItem> cartArrays)
        {
            // ReSharper disable once UnusedVariable
            //var shoppingCart = cartArrays;
            //return Redirect("/Products");
            var total = cartArrays.Sum(item => item.total);
            var order = new Order
            {
                CustomerId = 1,
                PaymentTypeId = Order.PaymentTypeEnum.Cod,
                ShipName = "Pham Anh Dung",
                ShipPhone = "0762941097",
                ShipAddress = "C6 Giang Vo",
                TotalPrice = total
            };
            var orderItems = new List<OrderItem>();
            var existError = false;
            foreach (var item in cartArrays)
            {
                var product = _db.Products.Find(item.id);
                if (product == null)
                {
                    existError = true;
                    break;
                }

                var orderItem = new OrderItem
                {
                    ProductId = product.Id, OrderId = order.Id, Quantity = item.count, UnitPrice = item.price
                };
                orderItems.Add(orderItem);
            }

            var redirectUrl = Url.Action("Index", "Home");
            if (existError) return Json(new {url = redirectUrl, message = "Fail"});
            order.OrderItems = orderItems;
            _db.Orders.Add(order);
            _db.SaveChanges();
            return Json(new { url = redirectUrl, message = "Success" });
        }

    }
}