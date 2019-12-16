using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASP.NET_Project.Models
{
    public class ASPNET_ProjectContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ASPNET_ProjectContext() : base("name=ASPNET_ProjectContext")
        {
        }

        public System.Data.Entity.DbSet<ASP.NET_Project.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<ASP.NET_Project.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<ASP.NET_Project.Models.Brand> Brands { get; set; }

        public System.Data.Entity.DbSet<ASP.NET_Project.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<ASP.NET_Project.ViewModels.ProductViewModel> ProductViewModels { get; set; }

        public System.Data.Entity.DbSet<ASP.NET_Project.Models.Customer> Customers { get; set; }
    }
}
