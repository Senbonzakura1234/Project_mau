using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ASP.NET_Project.Models
{
    public class MyDbContext : IdentityDbContext<User>
    {
        public MyDbContext() : base("name=MyContext")
        {

        }
        public static MyDbContext Create()
        {
            return new MyDbContext();
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}