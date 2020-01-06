// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
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
    public class ProductsController : Controller
    {
        private readonly MyDbContext _db = new MyDbContext();

        // GET: Products
        [HandleError]
        public ActionResult Index(int? advanceBrand, int? advanceCategory, string advanceName, 
            double? advancePriceFrom, double? advancePriceTo, int? page)
        {
            var predicate = PredicateBuilder.New<Product>(true);
            predicate = predicate.And(f => f.ProductStatus != Product.ProductStatusEnum.Deleted);
            Debug.WriteLine(advanceBrand);
            if (!string.IsNullOrEmpty(advanceName))
            {
                page = 1;
                Debug.WriteLine("okay");
                predicate = predicate.And(f => f.Name.Contains(advanceName));
                ViewBag.advanceName = advanceName;
            }


            if (advancePriceFrom >= 0 && advancePriceTo >= 0)
            {
                page = 1;
                predicate = advancePriceTo > 0 ?
                    predicate.And(f => f.Price <= advancePriceTo && f.Price >= advancePriceFrom) :
                    predicate.And(f => f.Price >= advancePriceFrom);
            }

            if (advanceBrand != null && advanceBrand > 0)
            {
                page = 1;
                predicate = predicate.And(f => f.BrandId == advanceBrand);
            }
            if (advanceCategory != null && advanceCategory > 0)
            {
                page = 1;
                predicate = predicate.And(f => f.CategoryId == advanceCategory);
            }

            

            var data = _db.Products.AsExpandable().Where(predicate);
            var lsProducts = new List<ProductViewModel>();
            var productViewModel = new ProductViewModel();
            var brandsList = productViewModel.BrandsList;
            var categoriesList = productViewModel.CategoriesList;
            ViewBag.brandsList = brandsList;
            ViewBag.categoriesList = categoriesList;
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

        public ActionResult AjaxProducts(int? advanceBrand,int? advanceCategory, string advanceName, 
            double? advancePriceFrom, double? advancePriceTo, int? page)
        {
            var predicate = PredicateBuilder.New<Product>(true);
            predicate = predicate.And(f => f.ProductStatus != Product.ProductStatusEnum.Deleted);
            Debug.WriteLine(advanceBrand);
            if (!string.IsNullOrEmpty(advanceName))
            {
                page = 1;
                Debug.WriteLine("okay");
                predicate = predicate.And(f => f.Name.Contains(advanceName));
                ViewBag.advanceName = advanceName;
            }


            if (advancePriceFrom >= 0 && advancePriceTo >= 0)
            {
                page = 1;
                predicate = advancePriceTo > 0 ?
                    predicate.And(f => f.Price <= advancePriceTo && f.Price >= advancePriceFrom) :
                    predicate.And(f => f.Price >= advancePriceFrom);
            }
            if (advanceBrand != null && advanceBrand > 0)
            {
                page = 1;
                predicate = predicate.And(f => f.BrandId == advanceBrand);
            }

            if (advanceCategory != null && advanceCategory > 0)
            {
                page = 1;
                predicate = predicate.And(f => f.CategoryId == advanceCategory);
            }
            var data = _db.Products.AsExpandable().Where(predicate);
            var lsProducts = new List<ProductViewModel>();
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
            const int pageSize = 12;
            var pageNumber = (page ?? 1);
            return PartialView("_AjaxProducts", lsProducts.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetChartData()
        {
            var data = _db.Products.Where(s => s.ProductStatus != Product.ProductStatusEnum.Deleted)
                .GroupBy(
                    s => new
                    {
                        s.CreatedAt.Year, s.CreatedAt.Month
        }
                ).Select(s => new
                {
                    YearData = s.FirstOrDefault().CreatedAt.Year,
                    MonthData = s.FirstOrDefault().CreatedAt.Month,
                    Count = s.Count()
                }).OrderBy(s => s.MonthData).ToList();
            return new JsonResult()
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


        // GET: Products/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                var newGuid = Guid.Parse(id);
                Console.WriteLine($@"Converted {id} to a Guid");
                var product = _db.Products.Find(newGuid);
                if (product == null)
                {
                    return HttpNotFound();
                }
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
                return View(data);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine(@"The string to be parsed is null.");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catch (FormatException)
            {
                Console.WriteLine($@"Bad format: {id}");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name");
            ViewBag.BrandId = new SelectList(_db.Brands, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from over-posting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,InStoke,BrandId,CategoryId,Picture,CreatedAt,UpdatedAt,DeletedAt,ProductStatus")] Product product)
        {
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name", product.CategoryId);
            ViewBag.BrandId = new SelectList(_db.Brands, "Id", "Name", product.BrandId);
            if (!ModelState.IsValid) return View(product);
            _db.Products.Add(product);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: Products/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                var newGuid = Guid.Parse(id);
                Console.WriteLine($@"Converted {id} to a Guid");
                var product = _db.Products.Find(newGuid);
                if (product == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name", product.CategoryId);
                ViewBag.BrandId = new SelectList(_db.Brands, "Id", "Name", product.BrandId);
                return View(product);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine(@"The string to be parsed is null.");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            catch (FormatException)
            {
                Console.WriteLine($@"Bad format: {id}");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
        }

        // POST: Products/Edit/5
        // To protect from over-posting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price,InStoke,BrandId,CategoryId,Picture,CreatedAt,UpdatedAt,DeletedAt,ProductStatus")] Product product)
        {
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name", product.CategoryId);
            ViewBag.BrandId = new SelectList(_db.Brands, "Id", "Name", product.BrandId);
            product.UpdatedAt = DateTime.Now;
            if (!ModelState.IsValid) return View(product);
            _db.Entry(product).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Products/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                var newGuid = Guid.Parse(id);
                Console.WriteLine($@"Converted {id} to a Guid");
                var product = _db.Products.Find(newGuid);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine(@"The string to be parsed is null.");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            catch (FormatException)
            {
                Console.WriteLine($@"Bad format: {id}");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                var newGuid = Guid.Parse(id);
                Console.WriteLine($@"Converted {id} to a Guid");
                var product = _db.Products.Find(newGuid);
                if (product == null) return RedirectToAction("Index");
                product.ProductStatus = Product.ProductStatusEnum.Deleted;
                product.UpdatedAt = DateTime.Now;
                product.DeletedAt = DateTime.Now;
                _db.Entry(product).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine(@"The string to be parsed is null.");
                return RedirectToAction("Index");
            }
            catch (FormatException)
            {
                Console.WriteLine($@"Bad format: {id}");
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
