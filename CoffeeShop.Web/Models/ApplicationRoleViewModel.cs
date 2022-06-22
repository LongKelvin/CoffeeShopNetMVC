using System.Collections.Generic;

namespace CoffeeShop.Web.Models
{
    public class ApplicationRoleViewModel
    {
        public ApplicationRoleViewModel()
        {
            PermissionIds = new List<string>();
        }

        public string Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public bool IsSystemProtected { get; set; }

        public List<string> PermissionIds { get; set; }
    }
}