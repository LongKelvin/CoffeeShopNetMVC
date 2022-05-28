using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

namespace CoffeeShop.Data.Repositories
{
    public interface IApplicationUserPermissionRepository : IRepository<ApplicationUserPermission>
    {
    }

    public class ApplicationUserPermissionRepository : RepositoryBase<ApplicationUserPermission>, IApplicationUserPermissionRepository
    {
        public ApplicationUserPermissionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}