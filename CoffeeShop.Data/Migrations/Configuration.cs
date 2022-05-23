namespace CoffeeShop.Data.Migrations
{
    using CoffeeShop.Models.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Text;
using static System.Data.Entity.Migrations.Model.UpdateDatabaseOperation;

    internal sealed class Configuration : DbMigrationsConfiguration<CoffeeShop.Data.CoffeeShopDbContext>
    {
        public Configuration()
{
            AutomaticMigrationsEnabled = true;
           AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(CoffeeShop.Data.CoffeeShopDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            CreateUser(context);
            CreateProductCategorySample(context);
            CreateProductsSample(context);
            CreateSlidesSample(context);
            CreatePagesSample(context);
            CreateShopInfo(context);
            CreatePaymentMethodSample(context);
        }

        private void CreateUser(CoffeeShopDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new CoffeeShopDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new CoffeeShopDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "admin",
                Email = "admin.testapp@gmail.com",
                EmailConfirmed = true,
                BirthDay = new DateTime(1999, 10, 10),
                FullName = "Adminstrators"
            };
            if (manager.Users.Count(x => x.UserName == "admin") == 0)
            {
                manager.Create(user, "admin@123X");

                if (!roleManager.Roles.Any())
                {
                    roleManager.Create(new IdentityRole { Name = "Admin" });
                    roleManager.Create(new IdentityRole { Name = "User" });
                }

                var adminUser = manager.FindByEmail("admin.testapp@gmail.com");

                manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
            }
        }

        private void CreateProductCategorySample(CoffeeShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
            {
                new ProductCategory() { Name="Cà phê pha máy",Alias="cf-phamay",Status=true },
                new ProductCategory() { Name="Cà phê phin",Alias="cf-phin",Status=true },
                new ProductCategory() { Name="Cà phê hạt",Alias="cf-hat",Status=true },
                new ProductCategory() { Name="Dụng cụ pha Cà Phê",Alias="cf-dungcu",Status=true }
            };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }
        }

        private void CreateProductsSample(CoffeeShopDbContext context)
        {
            if (context.Products.Count() == 0)
            {
                List<Product> listProduct = new List<Product>()
            {
                new Product() { Name="Pine Mountain Arabica 250g",Alias="cf-hat",Status=true, Price=175000 },
                new Product() { Name="Black Volcano Robusta 250g",Alias="cf-hat",Status=true ,Price=105000},
                new Product() { Name="Pine Mountain Fruity 250g",Alias="cf-hat",Status=true ,Price=190000},
                new Product() { Name="Pine Mountain Blend #7 250g",Alias="cf-hat",Status=true,Price=143000 },
                new Product() { Name="Pine Mountain Blend #5 250",Alias="cf-hat",Status=true ,Price=140000},
                new Product() { Name="Pine Mountain Arabica 500g",Alias="cf-hat",Status=true ,Price=335000},
                new Product() { Name="Black Volcano Robusta 500g",Alias="cf-hat",Status=true ,Price=195000},
                new Product() { Name="Pine Mountain Fruity 500g",Alias="cf-hat",Status=true ,Price=364000},
                new Product() { Name="Pine Mountain Blend #7 500g",Alias="cf-hat",Status=true ,Price=270000},
                new Product() { Name="Pine Mountain Blend #5 500g",Alias="cf-hat",Status=true ,Price=265000},
            };
                context.Products.AddRange(listProduct);
                context.SaveChanges();
            }
        }

        private void CreateSlidesSample(CoffeeShopDbContext context)
        {
            if (context.Slides.Count() == 0)
            {
                List<Slide> listSlide = new List<Slide>()
                {
                    new Slide(){ Status=true, Title = "Coffee Way", ActionName="Khám phá ngay1", Description="Uống Coffee Way, Uống sức khỏe", DisplayOrder = 1, Images = "/UploadFiles/images/slider-bg.jpg", URL="/Products"},
                    new Slide(){ Status=true, Title = "Coffee Way", ActionName="Khám phá ngay2", Description="Uống Coffee Way, Uống sức khỏe", DisplayOrder = 2, Images = "/UploadFiles/images/slider-bg.jpg", URL="/Products"},
                    new Slide(){ Status=true, Title = "Coffee Way", ActionName="Khám phá ngay3", Description="Uống Coffee Way, Uống sức khỏe", DisplayOrder = 3, Images = "/UploadFiles/images/slider-bg.jpg", URL="/Products"}
                };

                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }

        private void CreatePagesSample(CoffeeShopDbContext context)
        {
            if (context.Pages.Count() == 0)
            {
                Page page = new Page
                {
                    Name = "About Us",
                    Alias = "About",
                    Status = true,
                    Content = "ABOUT US"
                };

                context.Pages.Add(page);
                context.SaveChanges();
            }
        }

        private void CreateShopInfo(CoffeeShopDbContext context)
        {
            if (context.ShopInfos.Count() == 0)
            {
                ShopInformation shopInfo = new ShopInformation
                {
                    Name = "Coffee Way",
                    Address = "2B Bình Giã - Phường 13 - Tân Bình - TP. Hồ Chí Minh",
                    Latitude = 10.798109173100983,
                    Longitude = 106.64413230105687,
                    Status = true,
                    Email = "coffeewaytb@gmail.com",
                    Telephone = "0707635581",
                    Code = "03F53T"
                };

                context.ShopInfos.Add(shopInfo);
                context.SaveChanges();
            }
        }

        private void CreatePaymentMethodSample(CoffeeShopDbContext context)
        {
            if (context.PaymentMethods.Count() == 0)
            {
                List<PaymentMethod> listPaymentMethod = new List<PaymentMethod>() {
                    new PaymentMethod() {
                        Status = true,
                        PaymentName = "SHIP CODE",
                        PaymentCode = 100,
                        LogoImage = "/UploadFiles/images/icon-shipcod.png"
                    },

                    new PaymentMethod() {
                        Status = true,
                        PaymentName = "MOMO",
                        PaymentCode = 101,
                        LogoImage = "/UploadFiles/images/icon-momo.png"
                    },

                    new PaymentMethod() {
                        Status = true,
                        PaymentName = "ZALO PAY",
                        PaymentCode = 102,
                        LogoImage = "/UploadFiles/images/icon-zalopay.png"
                    },

                    new PaymentMethod() {
                        Status = true,
                        PaymentName = "INTERNET BANKING",
                        PaymentCode = 103,
                        LogoImage = "/UploadFiles/images/icon-bank.png"
                    },

                    new PaymentMethod() {
                        Status = true,
                        PaymentName = "CREDIT CARD",
                        PaymentCode = 104,
                        LogoImage = "/UploadFiles/images/icon-creditcard.png"
                    },
    };

                context.PaymentMethods.AddRange(listPaymentMethod);
                context.SaveChanges();
            }
        }

        
    }
}