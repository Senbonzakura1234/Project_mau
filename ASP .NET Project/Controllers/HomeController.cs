// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASP.NET_Project.Models;
using ASP.NET_Project.ViewModels;
using LinqKit;
using PagedList;

namespace ASP.NET_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyDbContext _db = new MyDbContext();

        [HandleError]
        public ActionResult Index(string searchTerm,  int? page)
        {
            var predicate = PredicateBuilder.New<Product>(true);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                page = 1;
                Debug.WriteLine("okay");
                predicate = predicate.Or(f => f.Name.Contains(searchTerm));
                ViewBag.searchTerm = searchTerm;
            }
            var data = _db.Products.AsExpandable().Where(predicate);
            var lsProducts = new List<ProductViewModel>();
            var productViewModel = new ProductViewModel();
            var brandsList = productViewModel.BrandsList;
            ViewBag.brandsList = brandsList;
            foreach (var product in data)
            {
                lsProducts.Add(new ProductViewModel(
                    product.Id,
                    product.Name,
                    product.Description,
                    product.Price,
                    product.InStoke,
                    product.CategoryId,
                    product.BrandId,
                    product.Picture
                ));
            }
            const int pageSize = 8;
            var pageNumber = (page ?? 1); 
            return View(lsProducts.ToPagedList(pageNumber, pageSize));
        }

        public JsonResult GetProductInfo(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var product = _db.Products.Find(id);
            return product != null
                ? Json(new
                {
                    id = product.Id,
                    name = product.Name,
                    price = product.Price,
                    picture = product.Picture
                }, JsonRequestBehavior.AllowGet)
                : null;
        }

        public ActionResult QuickView(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var product = _db.Products.Find(id);
            if (product == null) return null;
            var data = new ProductViewModel(
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.InStoke,
                product.CategoryId,
                product.BrandId,
                product.Picture
            );
            return View("_AjaxQuickView", data);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}