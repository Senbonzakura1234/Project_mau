using ASP.NET_Project.Models;
using LinqKit;
// ReSharper disable once RedundantUsingDirective
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ASP.NET_Project.Controllers
{
    public class CustomersController : Controller
    {
        private readonly MyDbContext _db = new MyDbContext();

        // GET: Customers
        public ActionResult Index(string searchTerm, string advanceFullname, int? advanceAge, string advanceEmail,
            int? advanceCheckValue)
        {
            //
            var predicate = PredicateBuilder.New<Customer>(true);
            Debug.WriteLine("advanceCheckValue" + advanceCheckValue);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                Debug.WriteLine("okay");
                predicate = predicate.Or(f => f.Fullname.Contains(searchTerm));
                predicate = predicate.Or(f => f.Email.Contains(searchTerm));
                ViewBag.searchTerm = searchTerm;
            }

            if (advanceCheckValue == 0)
            {
                if (!string.IsNullOrEmpty(advanceFullname))
                {
                    predicate = predicate.Or(f => f.Fullname.Contains(advanceFullname));
                    ViewBag.advanceFullname = advanceFullname;
                }

                if (!string.IsNullOrEmpty(advanceEmail))
                {
                    predicate = predicate.Or(f => f.Email.Contains(advanceEmail));
                    ViewBag.advanceEmail = advanceEmail;
                }

                if (advanceAge >= 0)
                {
                    predicate = predicate.Or(f => f.Age == advanceAge);
                    ViewBag.advanceAge = advanceAge;
                }
                ViewBag.advanceCheckValue = 0;
            }
            else
            {
                if (!string.IsNullOrEmpty(advanceFullname))
                {
                    predicate = predicate.And(f => f.Fullname.Contains(advanceFullname));
                    ViewBag.advanceFullname = advanceFullname;
                }

                if (!string.IsNullOrEmpty(advanceEmail))
                {
                    predicate = predicate.And(f => f.Email.Contains(advanceEmail));
                    ViewBag.advanceEmail = advanceEmail;
                }

                if (advanceAge >= 0)
                {
                    predicate = predicate.And(f => f.Age == advanceAge);
                    ViewBag.advanceAge = advanceAge;
                }
                ViewBag.advanceCheckValue = 1;
            }

            var data = _db.Customers.AsExpandable().Where(predicate);
            return View(data);
        }
        
        public ActionResult AjaxCustomers(string advanceFullname, int? advanceAge, string advanceEmail,
            int? advanceCheckValue)
        {
            var predicate = PredicateBuilder.New<Customer>(true);
            Debug.WriteLine("advanceCheckValue" + advanceCheckValue);

            if (advanceCheckValue == 0)
            {
                if (!string.IsNullOrEmpty(advanceFullname))
                {
                    predicate = predicate.Or(f => f.Fullname.Contains(advanceFullname));
                    ViewBag.advanceFullname = advanceFullname;
                }

                if (!string.IsNullOrEmpty(advanceEmail))
                {
                    predicate = predicate.Or(f => f.Email.Contains(advanceEmail));
                    ViewBag.advanceEmail = advanceEmail;
                }

                if (advanceAge >= 0)
                {
                    predicate = predicate.Or(f => f.Age == advanceAge);
                    ViewBag.advanceAge = advanceAge;
                }
                ViewBag.advanceCheckValue = 0;
            }
            else
            {
                if (!string.IsNullOrEmpty(advanceFullname))
                {
                    predicate = predicate.And(f => f.Fullname.Contains(advanceFullname));
                    ViewBag.advanceFullname = advanceFullname;
                }

                if (!string.IsNullOrEmpty(advanceEmail))
                {
                    predicate = predicate.And(f => f.Email.Contains(advanceEmail));
                    ViewBag.advanceEmail = advanceEmail;
                }

                if (advanceAge >= 0)
                {
                    predicate = predicate.And(f => f.Age == advanceAge);
                    ViewBag.advanceAge = advanceAge;
                }
                ViewBag.advanceCheckValue = 1;
            }

            var data = _db.Customers.AsExpandable().Where(predicate);
            return PartialView("_AjaxCustomers", data);
        }
        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = _db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from over-posting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fullname,Email,Phone,Password,ConfirmPassword,Age,Gender,Status,CreatedAt,UpdatedAt,DeletedAt")] Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);
            _db.Customers.Add(customer);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = _db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from over-posting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fullname,Email,Phone,Password,ConfirmPassword,Age,Gender,Status,CreatedAt,UpdatedAt,DeletedAt")] Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);
            _db.Entry(customer).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = _db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var customer = _db.Customers.Find(id);
            if (customer != null) _db.Customers.Remove(customer);
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
