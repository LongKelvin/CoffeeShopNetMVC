using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Data.Repositories
{
    public interface IApplicationUserPermissionRepository : IRepository<ApplicationUserPermission>
    {
        void AddPermissionToUser(string userId, ApplicationRole appRole, List<string> listPermissionId);
    }

    public class ApplicationUserPermissionRepository : RepositoryBase<ApplicationUserPermission>, IApplicationUserPermissionRepository
    {
        public ApplicationUserPermissionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public void AddPermissionToUser(string userId, ApplicationRole appRole, List<string> listPermissionId)
        {
            var listPermissionUser = new List<ApplicationUserPermission>();
            foreach (var permissionId in listPermissionId)
            {
                listPermissionUser.Add(new ApplicationUserPermission
                {
                    UserId = userId,
                    PermissionId = permissionId,
                    RoleId = appRole.Id,
                    RoleName = appRole.Name
                });
            }

            DbContext.ApplicationUserPermissions.AddRange(listPermissionUser);
        }
    }
}