using System.Collections.Generic;

namespace CoffeeShop.Common
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            };
        }

        public static List<string> GetDefaultApplicationModuleForSuperAdmin()
        {
            return new List<string>()
            {
                "Products",
                "ProductCategories",
                "Posts",
                "Blogs",
                "ShopInformations",
                "Orders",
                "OrderDetails",
                "Menus",
                "Pages",
                "SupportOnlines",
                "Slides",
                "Footers",
                "FeedBacks",
                "ApplicationUsers",
                "ApplicationGroups",
                "ApplicationRoles",
                "VisitorStatics"
            };
        }

        public static List<string> GetDefaultApplicationModuleForAdmin()
        {
            return new List<string>()
            {
                "Products",
                "ProductCategories",
                "Posts",
                "Blogs",
                "ShopInformations",
                "Orders",
                "OrderDetails",
                "Slides",
                "Footers",
                "FeedBacks",
                "ApplicationUsers",
                "VisitorStatics"
            };
        }

        public static List<string> GetDefaultApplicationModuleForBasicUser()
        {
            return new List<string>()
            {
                "Products",
                "ProductCategories",
                "Posts",
                "Blogs",
            };
        }

        public static List<string> GenerateAllDefaultApplicationPermission()
        {
            List<string> listAllPermission = new List<string>();
            var defaultApplicationModule = GetDefaultApplicationModuleForSuperAdmin();
            foreach (var module in defaultApplicationModule)
            {
                var temp = GeneratePermissionsForModule(module);

                foreach (var permission in temp)
                {
                    listAllPermission.Add(permission);
                }
            }
            return listAllPermission;
        }
    }
}