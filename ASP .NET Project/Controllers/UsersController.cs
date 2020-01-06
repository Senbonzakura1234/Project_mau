using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ASP.NET_Project;
using ASP.NET_Project.Models;

namespace ASP.NET_Project.Controllers
{
    public class UsersController : Controller
    {
        private readonly MyDbContext _db;

        private readonly IdentityConfig.MyUserManager _userManager;

        //private RoleManager<UserRole> RoleManager;



        public UsersController(MyDbContext dbContext, IdentityConfig.MyUserManager myUserManager)
        {
            _db = dbContext;
            _userManager = myUserManager;
        }

        //[Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var data = _db.Users.ToList();
            ViewBag.Data = data;
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.ContentSubTitle = "Sign in to continue.";
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password, int? keepSignIn)
        {
            Debug.WriteLine(keepSignIn);
            var user = _userManager.Find(username, password);
            if (user == null)
            {
                ViewBag.ContentSubTitle = "Login failed! Incorrect username or password.";
                ViewBag.LoginFail = "text-danger";
                return View();
            }
            
            

            var ident = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            var authManager = HttpContext.GetOwinContext().Authentication;
            var isPersistent = keepSignIn == 1;
            authManager.SignIn(
                new AuthenticationProperties { IsPersistent = isPersistent }, ident);

            return RedirectToAction("Index","DashBoard");
        }

        //[Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "DashBoard");
        }

        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Register(string username, string password, string email)
        {
            //if (_userManager.FindByName(username) != null) return View();
            var user = new User
            {
                UserName = username,
                Birthday = DateTime.Now,
                Email = email,
                Avatar = "https://res.cloudinary.com/senbonzakura/image/upload/v1573316680/images/faces/avatar2_p9btpy.png"
            };
            var result = _userManager.Create(user, password);
            if (!result.Succeeded)
            {
                Debug.WriteLine("failed");
                Debug.WriteLine(result.Errors);
                return View();
            }


            _userManager.AddToRole(user.Id, "Admin");


            //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
            await _userManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"http://google.com.vn\">here</a>");



            var userLogin = _userManager.Find(user.UserName, password);
            if (userLogin == null)
            {
                return HttpNotFound();
            }

            var ident = _userManager.CreateIdentity(userLogin, DefaultAuthenticationTypes.ApplicationCookie);
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignIn(
                new AuthenticationProperties { IsPersistent = false }, ident);

            return RedirectToAction("Index", "DashBoard");


        }
    }
}