using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASP.NET_Project.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using static ASP.NET_Project.IdentityConfig;

[assembly: OwinStartup(typeof(ASP.NET_Project.Startup))]
namespace ASP.NET_Project
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<MyDbContext>(MyDbContext.Create);
            app.CreatePerOwinContext<MyUserManager>(MyUserManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/User/Login")
            });
        }
    }
}