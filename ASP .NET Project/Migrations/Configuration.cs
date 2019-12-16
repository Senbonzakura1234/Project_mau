using ASP.NET_Project.Models;
// ReSharper disable RedundantUsingDirective

namespace ASP.NET_Project.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    // ReSharper disable once UnusedMember.Global
    internal sealed class Configuration : DbMigrationsConfiguration<MyDbContext>
    {
        private Random gen = new Random();

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        public class ProductSeed
        {
            public int BrandId { get; set; }
            public int CategoryId { get; set; }
            public string Description { get; set; }
            public int InStoke { get; set; }
            public double Price { get; set; }
            public string Name { get; set; }
            public string Picture { get; set; }
        }

        public DateTime RandomDay(int type)
        {
            if (type == 1)
            {
                var start = new DateTime(2016, 1, 1);
                var range = (DateTime.Today.AddDays(-1) - start).Days;
                return start.AddDays(gen.Next(range));
            }
            else
            {
                var start = new DateTime(2011, 1, 1);
                var end = new DateTime(2015, 12,31);
                var range = (DateTime.Today - end).Days;
                return start.AddDays(gen.Next(range));
            }
        }
        protected override void Seed(MyDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var randomCustomerId = new Random();
            var randomCartCount = new Random();
            var allowedPriceValue = new[] { 
                0, 13, 109.46, 177.11, 286.57, 
                463.68, 89, 1.44, 2.33, 3.77,
                6.10, 1, 2, 3, 5, 8, 9.87, 15.97, 
                25.84, 41.81, 67.65, 750.25,
                1213.93, 1964.18, 3178.11, 21, 
                34, 55 };
            var randomCartPrice = new Random();
            var paymentTypeValue = Enum.GetValues(typeof(Order.PaymentTypeEnum));
            var randomPaymentType = new Random();
            var orderStatusValue = Enum.GetValues(typeof(Order.OrderStatusEnum));
            var randomOrderStatus = new Random();

           

           
            context.Brands.AddOrUpdate(x => x.Id, 
                new Brand {  Description = "Sony", Name = "Sony" },
                new Brand {  Description = "Apple", Name = "Apple" },
                new Brand {  Description = "Samsung", Name = "Samsung" }
            );
            context.Categories.AddOrUpdate(x => x.Id, 
                new Category {  Description = "TV", Name = "TV" },
                new Category {  Description = "Laptop", Name = "Laptop" },
                new Category {  Description = "Smartphone", Name = "Smartphone" }
            );

            var productSeed1 = new ProductSeed
            {
                BrandId = 1,
                CategoryId = 1,
                Description = "Sony Smart TV",
                InStoke = 112,
                Price = 569,
                Name = "Sony Bravia x5800 4K",
                Picture = "https://res.cloudinary.com/senbonzakura/image/upload/v1575638114/images/product/kisspng-sony-bravia-x8500d-4k-resolution-smart-tv-_E7_B4_A2_E5_B0_BC-kd-65-www-galleryhip-com-the-hippest-pics-5b63824626ee91.3235740915332480701595_y0bpfm.jpg"
            };
            var productSeed2 = new ProductSeed
            {
                BrandId = 2,
                CategoryId = 2,
                Description = "Macbook",
                InStoke = 358,
                Price = 1020,
                Name = "Macbook Air",
                Picture = "https://res.cloudinary.com/senbonzakura/image/upload/v1574425696/images/product/34850080_OVR_d3uppt.png"
            };
            var productSeed3 = new ProductSeed
            {
                BrandId = 3,
                CategoryId = 3,
                Description = "Samsung Smartphone",
                InStoke = 132,
                Price = 878,
                Name = "Samsung Galaxy S10+",
                Picture = "https://res.cloudinary.com/senbonzakura/image/upload/v1573643062/images/product/product_qisxsg.png"
            };
            var allowedProductSeed = new[] { productSeed1, productSeed2, productSeed3 };
            var randomProductSeed = new Random();
            for (var i = 0; i < 180; i++)
            {
                var date = RandomDay(0);
                var productToSeed = allowedProductSeed[randomProductSeed.Next(allowedProductSeed.Length)];
                context.Products.AddOrUpdate(x => x.Id,
                    new Product
                    {
                        BrandId = productToSeed.BrandId,
                        CategoryId = productToSeed.CategoryId,
                        Description = productToSeed.Description,
                        InStoke = productToSeed.InStoke,
                        Price = productToSeed.Price,
                        Name = productToSeed.Name,
                        Picture = productToSeed.Picture,
                        CreatedAt = date,
                        UpdatedAt = date
                    }
                );
            }

            context.Customers.AddOrUpdate(x => x.Id,
                new Customer
                {
                    Fullname = "Phạm Anh Dũng",
                    Email = "anhdungpham090@gmail.com",
                    Phone = "0762941097",
                    Password = "12345678",
                    ConfirmPassword = "12345678",
                    Age = 23,
                    Gender = Customer.GenderEnum.Male
                },
                new Customer
                {
                    Fullname = "Senbonzakura",
                    Email = "senbonzakura@gmail.com",
                    Phone = "0762941097",
                    Password = "12345678",
                    ConfirmPassword = "12345678",
                    Age = 24,
                    Gender = Customer.GenderEnum.Private
                },
                new Customer
                {
                    Fullname = "James Mike",
                    Email = "dungpath1805040@fpt.edu.vn",
                    Phone = "0762941097",
                    Password = "12345678",
                    ConfirmPassword = "12345678",
                    Age = 23,
                    Gender = Customer.GenderEnum.Private
                }
            );
            
            for (var j = 0; j < 3000; j++)
            {
                var cartCount = randomCartCount.Next(1, 2);
                double totalPrice = 0;
                for (var i = 0; i < cartCount; i++)
                {
                    var itemCount = randomCartCount.Next(1, 3);
                    var cartItem = new CartItem
                    {
                        count = itemCount,
                        price = allowedPriceValue[randomCartPrice.Next(allowedPriceValue.Length)]
                    };

                    totalPrice += itemCount * cartItem.price;
                }

                var customerId = randomCustomerId.Next(1, 3);
                var date = RandomDay(1);
                var paymentTypeEnum = (Order.PaymentTypeEnum)paymentTypeValue.GetValue(randomPaymentType.Next(paymentTypeValue.Length));
                var orderStatusEnum = (Order.OrderStatusEnum)orderStatusValue.GetValue(randomOrderStatus.Next(orderStatusValue.Length));



                context.Orders.AddOrUpdate(x => x.Id,
                    new Order
                    {
                        CustomerId = customerId,
                        TotalPrice = totalPrice,
                        ShipName = "Phạm Anh Dũng",
                        ShipAddress = "C6 506 Giang Vo",
                        ShipPhone = "0762941097",
                        CreatedAt = date,
                        UpdatedAt = date,
                        OrderStatus = orderStatusEnum,
                        PaymentTypeId = paymentTypeEnum
                    }
                );
            }

            //var orderItemList = listCart.Select(item => new OrderItem
            //{
            //    OrderId = 1, ProductId = item.id, Quantity = item.count, UnitPrice = item.price
            //}).ToList();
            //context.OrderItems.AddRange(orderItemList);
            //context.SaveChanges();
            //foreach (var item in listCart)
            //{
            //    context.OrderItems.AddOrUpdate(
            //        new OrderItem
            //        {
            //            OrderId = 1,
            //            ProductId = item.id,
            //            Quantity = item.count,
            //            UnitPrice = item.price
            //        }
            //    );
            //}
        }
    }
}
