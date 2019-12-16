using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASP.NET_Project.Models;

namespace ASP.NET_Project.Controllers
{
    public class CartController : Controller
    {
        private readonly MyDbContext _db = new MyDbContext();

        // GET: Cart
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Add(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}