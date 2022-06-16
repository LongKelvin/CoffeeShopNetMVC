using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

namespace CoffeeShop.Data.Repositories
{
    public interface IApplicattionRolePermissionRepository : IRepository<ApplicationRolePermission>
    {
        bool AddPermissionToRole(string roleId, string permissionId);
    }

    public class ApplicationRolePermissionRepository : RepositoryBase<ApplicationRolePermission>, IApplicattionRolePermissionRepository
    {
        public ApplicationRolePermissionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public bool AddPermissionToRole(string roleId, string permissionId)
        {
            var rolePermission = new ApplicationRolePermission
            {
                PermissionId = permissionId,
                RoleId = roleId
            };

            try
            {
                DbContext.ApplicationRolePermissions.Add(rolePermission);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}