using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeShop.Data.Repositories
{
    public interface IApplicationPermissionRepository : IRepository<ApplicationPermission>
    {
        ApplicationPermission CreateNewPermission(ApplicationPermission applicationPermission);

        List<ApplicationPermission> GetListPermissionByUserId(string userId);

        ApplicationPermission GetById(string id);

        bool DeletePermission(string id);

        List<ApplicationUser> GetListUserByPermissionId(string permissionId);

        List<ApplicationPermission> GetListPermissionByUserName(string userName);

        CoffeeShopDbContext GetDbContext();
    }

    public class ApplicationPermissionRepository : RepositoryBase<ApplicationPermission>, IApplicationPermissionRepository
    {
        public ApplicationPermissionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public  CoffeeShopDbContext GetDbContext()
        {
            return DbContext;
        }

        public ApplicationPermission CreateNewPermission(ApplicationPermission applicationPermission)
        {
            applicationPermission.Id = Guid.NewGuid().ToString();
            DbContext.ApplicationPermissions.Add(applicationPermission);
            DbContext.SaveChanges();
            return DbContext.ApplicationPermissions.Find(applicationPermission.Id);
        }

        public bool DeletePermission(string id)
        {
            bool result = false;
            try
            {
                //Get all role that contain delete permission
                var listRolePermissions = DbContext.ApplicationRolePermissions
                     .Where(x => x.PermissionId == id);

                DbContext.ApplicationRolePermissions.RemoveRange(listRolePermissions);

                //Get all user that have permission with id
                var listUserByPermission = DbContext.ApplicationUserPermissions
                    .Where(x => x.PermissionId == id);

                DbContext.ApplicationUserPermissions.RemoveRange(listUserByPermission);

                //Delete permission belong to this id
                DbContext.ApplicationPermissions.Remove(GetById(id));

                DbContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public ApplicationPermission GetById(string id)
        {
            return DbContext.ApplicationPermissions.Find(id);
        }

        public List<ApplicationPermission> GetListPermissionByUserId(string userId)
        {
            var query = from p in DbContext.ApplicationPermissions
                        join up in DbContext.ApplicationUserPermissions
                        on p.Id equals up.PermissionId
                        where up.UserId == userId
                        select p;

            return query.ToList();
        }

        public List<ApplicationPermission> GetListPermissionByUserName(string userName)
        {
            var userId = DbContext.Users.Where(x => x.UserName.Equals(userName)).FirstOrDefault().Id;
            return GetListPermissionByUserId(userId);
        }

        public List<ApplicationUser> GetListUserByPermissionId(string permissionId)
        {
            var query = from u in DbContext.Users
                        join up in DbContext.ApplicationUserPermissions
                        on u.Id equals up.UserId
                        where up.UserId == permissionId
                        select u;

            return query.ToList();
        }
    }
}