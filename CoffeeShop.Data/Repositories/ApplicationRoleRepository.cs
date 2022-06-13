using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

using Microsoft.AspNet.Identity.EntityFramework;

using System.Collections.Generic;
using System.Linq;

namespace CoffeeShop.Data.Repositories
{
    public interface IApplicationRoleRepository : IRepository<ApplicationRole>
    {
        IEnumerable<ApplicationRole> GetListRoleByGroupId(int groupId);

        ApplicationRole GetByStringId(string id);

        void DeleteUserInRole(string roleId);

        List<string> GetListUserIdByRoleId(string id);

        List<ApplicationUser> GetListUserByRoleId(string id);
    }

    public class ApplicationRoleRepository : RepositoryBase<ApplicationRole>, IApplicationRoleRepository
    {
        public ApplicationRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public void DeleteUserInRole(string roleId)
        {
            var userInRole = DbContext.Set<IdentityUserRole>()
                .Where(x => x.RoleId.Equals(roleId)).ToList();
            DbContext.Set<IdentityUserRole>().RemoveRange(userInRole);
        }

        public ApplicationRole GetByStringId(string id)
        {
            return DbContext.ApplicationRoles.SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<ApplicationRole> GetListRoleByGroupId(int groupId)
        {
            var query = from g in DbContext.ApplicationRoles
                        join ug in DbContext.ApplicationRoleGroups
                        on g.Id equals ug.RoleId
                        where ug.GroupId == groupId
                        select g;
            return query;
        }

        public List<ApplicationUser> GetListUserByRoleId(string id)
        {
            var listUserId = GetListUserIdByRoleId(id);

            return DbContext.Users.Where(x => listUserId.Contains(x.Id)).ToList();
        }

        public List<string> GetListUserIdByRoleId(string id)
        {
            return DbContext.Set<IdentityUserRole>()
                .Where(x => x.RoleId.Equals(id)).Select(u => u.UserId).ToList();
        }
    }
}