using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

using System.Collections.Generic;
using System.Linq;

namespace CoffeeShop.Data.Repositories
{
    public interface IApplicationGroupRepository : IRepository<ApplicationGroup>
    {
        List<ApplicationGroup> GetListGroupByUserId(string userId);

        List<ApplicationUser> GetListUserByGroupId(int groupId);
    }

    public class ApplicationGroupRepository : RepositoryBase<ApplicationGroup>, IApplicationGroupRepository
    {
        public ApplicationGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public List<ApplicationGroup> GetListGroupByUserId(string userId)
        {
            var query = from g in DbContext.ApplicationGroups
                        join ug in DbContext.ApplicationUserGroups
                        on g.ID equals ug.GroupId
                        where ug.UserId == userId
                        select g;

            return query.ToList();
        }

        public List<ApplicationUser> GetListUserByGroupId(int groupId)
        {
            var query = from g in DbContext.ApplicationGroups
                        join ug in DbContext.ApplicationUserGroups
                        on g.ID equals ug.GroupId
                        join u in DbContext.Users
                        on ug.UserId equals u.Id
                        where ug.GroupId == groupId
                        select u;
            return query.ToList();
        }
    }
}