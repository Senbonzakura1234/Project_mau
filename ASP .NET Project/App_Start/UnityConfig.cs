using System.Data.Entity;
using System.Web.Mvc;
using ASP.NET_Project.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Unity;
using Unity.Mvc5;

namespace ASP.NET_Project
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            container.RegisterType<IUserStore<User>, UserStore<User>>();
            container.RegisterType<UserManager<User>>();
            container.RegisterType<DbContext, MyDbContext>();
            container.RegisterType<IdentityConfig.MyUserManager>();
        }
    }
}