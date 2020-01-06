using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.NET_Project.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ASP.NET_Project.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UserRolesController : Controller
    {
        private readonly MyDbContext _db = new MyDbContext();
        private readonly RoleManager<UserRole> _roleManager;

        public UserRolesController()
        {
            var roleStore = new RoleStore<UserRole>(_db);
            _roleManager = new RoleManager<UserRole>(roleStore);
        }

        public ActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddRole(string name)
        {
            var role = new UserRole
            {
                Name = name,
                CreatedAt = DateTime.Now,
                Description = "User is " + name
            };
            var result = _roleManager.Create(role);
            return View();
        }
    }
}