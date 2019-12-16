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
using ASP.NET_Project.ViewModels;
using LinqKit;

namespace ASP.NET_Project.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly MyDbContext _db = new MyDbContext();

        // GET: Categories
        public ActionResult Index()
        {
            var predicate = PredicateBuilder.New<Category>(true);
            predicate = predicate.And(f => f.CategoryStatus != Category.CategoryStatusEnum.Deleted);
            var data = _db.Categories.AsExpandable().Where(predicate);
            var lsCategories = new List<CategoryViewModel>();
            foreach (var category in data)
            {
                lsCategories.Add(
                    new CategoryViewModel(
                        category.Id,
                        category.Name,
                        category.Description
                    )
                );
            }
            return View(lsCategories);
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,CreatedAt,UpdatedAt,DeletedAt,CategoryStatus")] Category category)
        {
            if (!ModelState.IsValid) return View(category);
            _db.Categories.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,CreatedAt,UpdatedAt,DeletedAt,CategoryStatus")] Category category)
        {
            if (!ModelState.IsValid) return View(category);
            category.UpdatedAt = DateTime.Now;
            category.CategoryStatus = Category.CategoryStatusEnum.Show;
            _db.Entry(category).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var category = _db.Categories.Find(id);
            if (category == null) return RedirectToAction("Index");
            category.CategoryStatus = Category.CategoryStatusEnum.Deleted;
            category.UpdatedAt = DateTime.Now;
            category.DeletedAt = DateTime.Now;
            _db.Entry(category).State = EntityState.Modified;
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
