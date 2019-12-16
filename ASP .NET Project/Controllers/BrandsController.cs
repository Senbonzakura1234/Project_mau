// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASP.NET_Project.Models;
using LinqKit;

namespace ASP.NET_Project.Controllers
{
    public class BrandsController : Controller
    {
        private readonly MyDbContext _db = new MyDbContext();

        // GET: Brands
        public ActionResult Index()
        {
            var predicate = PredicateBuilder.New<Brand>(true);
            predicate = predicate.And(f => f.BrandStatus != Brand.BrandStatusEnum.Deleted);
            var data = _db.Brands.AsExpandable().Where(predicate);
            return View(data);
        }

        // GET: Brands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var brand = _db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // GET: Brands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,CreatedAt,UpdatedAt,DeletedAt,BrandStatus")] Brand brand)
        {
            if (!ModelState.IsValid) return View(brand);
            _db.Brands.Add(brand);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: Brands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var brand = _db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,CreatedAt,UpdatedAt,DeletedAt,BrandStatus")] Brand brand)
        {
            if (!ModelState.IsValid) return View(brand);
            brand.UpdatedAt = DateTime.Now;
            brand.BrandStatus = Brand.BrandStatusEnum.Show;
            _db.Entry(brand).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Brands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var brand = _db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var brand = _db.Brands.Find(id);
            if (brand == null) return RedirectToAction("Index");
            brand.BrandStatus = Brand.BrandStatusEnum.Deleted;
            brand.UpdatedAt = DateTime.Now;
            brand.DeletedAt = DateTime.Now;
            _db.Entry(brand).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
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
