namespace CoffeeShop.Data.Migrations
{
    using CoffeeShop.Common;
    using CoffeeShop.Models.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<CoffeeShop.Data.CoffeeShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(CoffeeShop.Data.CoffeeShopDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            //ResetDatabaseIdentityRecord(context);

            CreateAppDefaultPermission(context);
            CreateUserAndRoles(context);

            CreateProductCategorySample(context);
            CreateProductsSample(context);
            CreateSlidesSample(context);
            CreatePagesSample(context);
            CreateShopInfo(context);
            CreatePaymentMethodSample(context);
        }

        private void CreateAppDefaultPermission(CoffeeShopDbContext context)
        {
            if (context.ApplicationPermissions.Count() == 0)
            {
                List<ApplicationPermission> appPermissions = new List<ApplicationPermission>();
                var listPermissonName = Permissions.GenerateAllDefaultApplicationPermission();

                foreach (var permission in listPermissonName)
                {
                    var appPermission = new ApplicationPermission
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = permission,
                        Type = StringHelper.GetStringAfterLasCharacter(permission, "."),
                        Module = StringHelper.GetStringBetween(permission, "."),
                        Description = $"Default App Permission for {permission}",
                        IsSystemProtected = true
                    };

                    appPermissions.Add(appPermission);
                }

                context.ApplicationPermissions.AddRange(appPermissions);
                context.SaveChanges();
            }
        }

        private void CreateUserAndRoles(CoffeeShopDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new CoffeeShopDbContext()));

            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new CoffeeShopDbContext()));

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new ApplicationRole
                {
                    Name = BasicRoles.SuperAdmin.ToString(),
                    Description = BasicRoles.SuperAdmin.ToString(),
                    IsSystemProtected = true
                });
                roleManager.Create(new ApplicationRole
                {
                    Name = BasicRoles.Admin.ToString(),
                    Description = BasicRoles.Admin.ToString(),
                    IsSystemProtected = true
                });
                roleManager.Create(new ApplicationRole
                {
                    Name = BasicRoles.BasicUser.ToString(),
                    Description = BasicRoles.BasicUser.ToString(),
                    IsSystemProtected = false
                });

                AddPermissionForRole(BasicRoles.SuperAdmin.ToString(), roleManager, context);
                AddPermissionForRole(BasicRoles.Admin.ToString(), roleManager, context);
                AddPermissionForRole(BasicRoles.BasicUser.ToString(), roleManager, context);
            }

            if (manager.Users.Count(x => x.UserName == "admin") == 0)
            {
                var admin = new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "admin.testapp@gmail.com",
                    EmailConfirmed = true,
                    BirthDay = new DateTime(1999, 10, 10),
                    FullName = "Adminstrators",
                    CreatedDate = DateTime.Now.Date,
                    Address = "HCMC VietNam",
                    PhoneNumber = "0984426795"
                };
                manager.Create(admin, "admin@123X");

                var adminUser = manager.FindById(admin.Id);
                manager.AddToRoles(adminUser.Id, new string[]
                {
                   BasicRoles.Admin.ToString(),
                   BasicRoles.BasicUser.ToString()
                });

                //AddUserClaim(adminUser, manager, context);
                AddPermissionForUser(adminUser, manager, roleManager, context);
            }

            if (manager.Users.Count(x => x.UserName == "superAdmin") == 0)
            {
                var superAdmin = new ApplicationUser()
                {
                    UserName = "superAdmin",
                    Email = "superAdmin.testapp@gmail.com",
                    EmailConfirmed = true,
                    BirthDay = new DateTime(1999, 10, 10),
                    FullName = "Super Adminstrators",
                    CreatedDate = DateTime.Now.Date,
                    Address = "HCMC VietNam",
                    PhoneNumber = "0984426796"
                };
                manager.Create(superAdmin, "superAdmin@123X");

                var superUser = manager.FindById(superAdmin.Id);
                manager.AddToRoles(superUser.Id, new string[]
                {
                    BasicRoles.SuperAdmin.ToString(),
                    BasicRoles.Admin.ToString(),
                    BasicRoles.BasicUser.ToString()
                });

                //AddUserClaim(superUser, manager, context);
                AddPermissionForUser(superUser, manager, roleManager, context);
            }

            if (manager.Users.Count(x => x.UserName == "basicUser") == 0)
            {
                var basicUser = new ApplicationUser()
                {
                    UserName = "basicUser",
                    Email = "basicUser.testapp@gmail.com",
                    EmailConfirmed = true,
                    BirthDay = new DateTime(1999, 10, 10),
                    FullName = "Basic User",
                    CreatedDate = DateTime.Now.Date,
                    Address = "HCMC VietNam",
                    PhoneNumber = "0984426794"
                };
                manager.Create(basicUser, "basicUser@123X");

                var user = manager.FindById(basicUser.Id);
                manager.AddToRoles(user.Id, new string[]
                {
                    BasicRoles.BasicUser.ToString()
                });

                //AddUserClaim(user, manager, context);
                AddPermissionForUser(user, manager, roleManager, context);
            }
        }

        private static void AddPermissionForRole(string roleName, RoleManager<IdentityRole> roleManager, CoffeeShopDbContext context)
        {
            if (roleName.Equals(BasicRoles.SuperAdmin.ToString()))
            {
                var permissionForSuperAdmin = context.ApplicationPermissions
                    .Select(x => x.Id).ToList();
                var roleId = roleManager.FindByName(BasicRoles.SuperAdmin.ToString()).Id;

                foreach (var permission in permissionForSuperAdmin)
                {
                    context.ApplicationRolePermissions.Add(new ApplicationRolePermission()
                    {
                        PermissionId = permission,
                        RoleId = roleId
                    });
                }
            }
            else if (roleName.Equals(BasicRoles.Admin.ToString()))
            {
                var adminModules = Permissions.GetDefaultApplicationModuleForAdmin();
                var roleId = roleManager.FindByName(BasicRoles.Admin.ToString()).Id;
                var permissionForAdmin = context.ApplicationPermissions
                    .Where(x => adminModules.Contains(x.Module))
                    .Select(p => p.Id).ToList();
                foreach (var permission in permissionForAdmin)
                {
                    context.ApplicationRolePermissions.Add(new ApplicationRolePermission()
                    {
                        PermissionId = permission,
                        RoleId = roleId
                    });
                }
            }
            else
            {
                var basicUserModules = Permissions.GetDefaultApplicationModuleForBasicUser();
                var roleId = roleManager.FindByName(BasicRoles.BasicUser.ToString()).Id;
                var permissionForBasicUser = context.ApplicationPermissions
                    .Where(x => basicUserModules.Contains(x.Module))
                    .Select(p => p.Id).ToList();
                foreach (var permission in permissionForBasicUser)
                {
                    context.ApplicationRolePermissions.Add(new ApplicationRolePermission()
                    {
                        PermissionId = permission,
                        RoleId = roleId
                    });
                }
            }

            context.SaveChanges();
        }

        #region Claim

        //This function aim to add claim for user that has been seed when initialize in the first time
        //the application run
        private static void CreateClaimForBasicRole(string basicRole, List<string> appModules, CoffeeShopDbContext context, RoleManager<IdentityRole> roleManager)
        {
            var userRole = roleManager.FindByName(basicRole);
            AddPermissionClaim(userRole, appModules, context);
        }

        public static void AddPermissionClaim(IdentityRole role, List<string> modules, CoffeeShopDbContext context)
        {
            var allClaims = context.ApplicationRoleClaims.Where(x => x.RoleId.Equals(role.Id));

            List<string> allPermissions = new List<string>();

            foreach (var module in modules)
            {
                var tempPermission = Permissions.GeneratePermissionsForModule(module);
                foreach (var permission in tempPermission)
                {
                    allPermissions.Add(permission);
                }
            }

            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.ClaimType == "Permission" && a.ClaimValue == permission))
                {
                    context.ApplicationRoleClaims.Add(new ApplicationRoleClaims()
                    {
                        ClaimType = "Permission",
                        ClaimValue = permission,
                        RoleId = role.Id
                    });
                }
            }

            context.SaveChanges();
        }

        public static void AddUserClaim(ApplicationUser user, UserManager<ApplicationUser> userManager, CoffeeShopDbContext context)
        {
            List<ApplicationUserClaim> listUserClaims = new List<ApplicationUserClaim>();
            IDictionary<string, ApplicationRole> listUserRoles = new Dictionary<string, ApplicationRole>();

            var roleNameByUser = userManager.GetRoles(user.Id);

            foreach (var role in roleNameByUser)
            {
                var roleByRoleName = context.ApplicationRoles.Where(r => r.Name.Equals(role)).SingleOrDefault();

                try
                {
                    listUserRoles.Add(roleByRoleName.Id, roleByRoleName);
                }
                catch
                {
                    continue;
                }
            }

            foreach (var role in listUserRoles)
            {
                var claimByRole = context.ApplicationRoleClaims.Where(x => x.RoleId.Equals(role.Value.Id)).ToList();

                foreach (var claim in claimByRole)
                {
                    var userClaim = new ApplicationUserClaim
                    {
                        ClaimType = claim.ClaimType,
                        ClaimValue = claim.ClaimValue,
                        Id = claim.Id,
                        UserId = user.Id,
                    };

                    if (listUserClaims.Contains(userClaim))
                        continue;

                    listUserClaims.Add(userClaim);
                }
            }

            context.ApplicationUserClaims.AddRange(listUserClaims);
            context.SaveChanges();
        }

        #endregion Claim

        public void AddPermissionForUser(ApplicationUser user, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, CoffeeShopDbContext context)
        {
            List<ApplicationUserPermission> listAppUserPermission = new List<ApplicationUserPermission>();
            var roleNameByUser = userManager.GetRoles(user.Id);

            var listAppRoles = context.ApplicationRoles;

            var roleIdByName = listAppRoles
                .Where(x => roleNameByUser.Contains(x.Name))
                .Select(k => k.Id);

            var listRolePermissions = context.ApplicationRolePermissions;

            foreach (var roleId in roleIdByName)
            {
                var roleName = listAppRoles.FirstOrDefault(x => x.Id.Equals(roleId)).Name;

                var permissionByRole = listRolePermissions
                    .Where(x => x.RoleId.Equals(roleId))
                    .Select(i => i.PermissionId);

                foreach (var permission in permissionByRole)
                {
                    var appUserPermission = new ApplicationUserPermission
                    {
                        UserId = user.Id,
                        RoleId = roleId,
                        RoleName = roleName,
                        PermissionId = permission
                    };

                    listAppUserPermission.Add(appUserPermission);
                }
            }

            context.ApplicationUserPermissions.AddRange(listAppUserPermission);
            context.SaveChanges();
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

        //delete all record from role, user, group and claims
        private void ResetDatabaseIdentityRecord(CoffeeShopDbContext context)
        {
            StringBuilder queryBuilder = new StringBuilder("delete from [CoffeeShopDatabase_MVC].[dbo].ApplicationRoleClaims  ");

            queryBuilder.AppendLine("delete from [CoffeeShopDatabase_MVC].[dbo].ApplicationUserRoles  ");
            queryBuilder.AppendLine("delete from [CoffeeShopDatabase_MVC].[dbo].ApplicationUserPermissions  ");
            queryBuilder.AppendLine("delete from [CoffeeShopDatabase_MVC].[dbo].ApplicationRolePermissions  ");
            queryBuilder.AppendLine("delete from [CoffeeShopDatabase_MVC].[dbo].ApplicationUserGroups  ");
            queryBuilder.AppendLine("delete from [CoffeeShopDatabase_MVC].[dbo].[ApplicationRoles]  ");
            queryBuilder.AppendLine("delete from [CoffeeShopDatabase_MVC].[dbo].ApplicationUsers  ");
            queryBuilder.AppendLine("delete from [CoffeeShopDatabase_MVC].[dbo].ApplicationGroups  ");
            queryBuilder.AppendLine("delete from [CoffeeShopDatabase_MVC].[dbo].ApplicationPermissions  ");

            context.Database.ExecuteSqlCommand(queryBuilder.ToString());
        }
    }
}