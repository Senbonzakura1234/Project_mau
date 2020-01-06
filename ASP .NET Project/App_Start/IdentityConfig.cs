using System;
using ASP.NET_Project.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace ASP.NET_Project
{
    public class IdentityConfig
    {
        //public void Configuration(IAppBuilder app)
        //{
        //    app.UseCookieAuthentication(new CookieAuthenticationOptions
        //    {
        //        AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
        //        LoginPath = new PathString("/Users/Login"),
        //    });
        //}
        public class MyUserManager : UserManager<User>
        {
            public MyUserManager(IUserStore<User> store) : base(store)
            {

            }

            public static MyUserManager Create(IdentityFactoryOptions<MyUserManager> options, IOwinContext context)
            {
                var userManager = new MyUserManager(new UserStore<User>(new MyDbContext()))
                {
                    UserLockoutEnabledByDefault = true,
                    DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5),
                    MaxFailedAccessAttemptsBeforeLockout = 5
                };

                userManager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<User>
                {
                    Subject = "Security Code",
                    BodyFormat = "Your security code is {0}"
                });
                userManager.EmailService = new EmailService();
                var dataProtectionProvider = options.DataProtectionProvider;
                if (dataProtectionProvider != null)
                {
                    userManager.UserTokenProvider =
                        new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET Identity"));
                }
                return userManager;
            }
        }
    }
}