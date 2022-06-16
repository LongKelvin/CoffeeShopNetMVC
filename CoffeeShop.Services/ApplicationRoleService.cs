using CoffeeShop.Common.ExceptionHandler;
using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

using System.Collections.Generic;
using System.Linq;

namespace CoffeeShop.Services
{
    public interface IApplicationRoleService
    {
        ApplicationRole GetDetail(string id);

        IEnumerable<ApplicationRole> GetAll(int page, int pageSize, out int totalRow, string filter);

        IEnumerable<ApplicationRole> GetAll();

        ApplicationRole Add(ApplicationRole appRole);

        ApplicationRole GetByStringId(string id);

        bool Update(ApplicationRole AppRole);

        bool Update(ApplicationRole AppRole, List<string> listPermissionId);

        bool Delete(string id);

        //Add roles to a sepcify group
        bool AddRolesToGroup(IEnumerable<ApplicationRoleGroup> roleGroups, int groupId);

        //Get list role by group id
        IEnumerable<ApplicationRole> GetListRoleByGroupId(int groupId);

        void SaveChanges();

        bool AddPermissionToRole(string roleId, string permissionId);
    }

    public class ApplicationRoleService : IApplicationRoleService
    {
        private IApplicationRoleRepository _appRoleRepository;
        private IApplicationRoleGroupRepository _appRoleGroupRepository;
        private IApplicattionRolePermissionRepository _appRolePermissionRepository;
        private IApplicationUserPermissionRepository _applicationUserPermissionRepository;

        private IUnitOfWork _unitOfWork;

        public ApplicationRoleService(IUnitOfWork unitOfWork,
            IApplicationRoleRepository appRoleRepository,
            IApplicationRoleGroupRepository appRoleGroupRepository,
            IApplicattionRolePermissionRepository appRolePermissionRepository,
            IApplicationUserPermissionRepository applicationUserPermissionRepository)
        {
            this._appRoleRepository = appRoleRepository;
            this._appRoleGroupRepository = appRoleGroupRepository;
            this._unitOfWork = unitOfWork;
            this._appRolePermissionRepository = appRolePermissionRepository;
            this._applicationUserPermissionRepository = applicationUserPermissionRepository;
        }

        public ApplicationRole Add(ApplicationRole appRole)
        {
            if (_appRoleRepository.CheckContains(x => x.Description == appRole.Description))
                throw new NameDuplicatedException("Tên không được trùng");
            return _appRoleRepository.Add(appRole);
        }

        public bool AddRolesToGroup(IEnumerable<ApplicationRoleGroup> roleGroups, int groupId)
        {
            _appRoleGroupRepository.DeleteMulti(x => x.GroupId == groupId);
            foreach (var roleGroup in roleGroups)
            {
                _appRoleGroupRepository.Add(roleGroup);
            }
            return true;
        }

        public bool Delete(string id)
        {
            var deleteRole = _appRoleRepository.GetByStringId(id);
            if (deleteRole == null || deleteRole.IsSystemProtected)
                return false;

            try
            {
                _appRolePermissionRepository.DeleteMulti(x => x.RoleId.Equals(id));
                _appRoleRepository.DeleteUserInRole(id);
                _appRoleRepository.DeleteMulti(x => x.Id == id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<ApplicationRole> GetAll()
        {
            return _appRoleRepository.GetAll();
        }

        public IEnumerable<ApplicationRole> GetAll(int page, int pageSize, out int totalRow, string filter = null)
        {
            var query = _appRoleRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.Description.Contains(filter));

            totalRow = query.Count();
            return query.OrderBy(x => x.Description).Skip(page * pageSize).Take(pageSize);
        }

        public ApplicationRole GetDetail(string id)
        {
            return _appRoleRepository.GetByCondition(x => x.Id == id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public bool Update(ApplicationRole AppRole)
        {
            if (_appRoleRepository.CheckContains(x => x.Description == AppRole.Description && x.Id != AppRole.Id))
                throw new NameDuplicatedException("Name cannot be duplicate");
            _appRoleRepository.Update(AppRole);
            return true;
        }

        public IEnumerable<ApplicationRole> GetListRoleByGroupId(int groupId)
        {
            return _appRoleRepository.GetListRoleByGroupId(groupId);
        }

        public ApplicationRole GetByStringId(string id)
        {
            return _appRoleRepository.GetByStringId(id);
        }

        public bool AddPermissionToRole(string roleId, string permissionId)
        {
            return _appRolePermissionRepository.AddPermissionToRole(roleId, permissionId);
        }

        public bool AddPermissionToRole(string roleId, List<string> listPermissionId)
        {
            foreach (var permission in listPermissionId)
            {
                if (AddPermissionToRole(roleId, permission) == false)
                    return false;
            }
            return true;
        }

        public bool Update(ApplicationRole AppRole, List<string> listPermissionId)
        {
            try
            {
                _appRolePermissionRepository.DeleteMulti(x => x.RoleId.Equals(AppRole.Id));
                var addPermissionResult = AddPermissionToRole(AppRole.Id, listPermissionId);
                if (addPermissionResult == false)
                    return false;

                var userIdByRoles = _appRoleRepository.GetListUserIdByRoleId(AppRole.Id);

                //Delete old permission_users
                _applicationUserPermissionRepository.DeleteMulti(x => userIdByRoles.Contains(x.UserId) && x.RoleId.Equals(AppRole.Id));

                //Update new
                foreach (var user in userIdByRoles)
                {
                    _applicationUserPermissionRepository.AddPermissionToUser(user, AppRole, listPermissionId);
                }

                SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}