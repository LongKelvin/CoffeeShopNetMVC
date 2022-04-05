namespace CoffeeShop.Data.Migrations
{
    using CoffeeShop.Models.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CoffeeShop.Data.CoffeeShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CoffeeShop.Data.CoffeeShopDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            CreateUser(context);
            CreateProductCategorySample(context);
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
    }
}