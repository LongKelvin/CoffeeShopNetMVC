using CoffeeShop.Common.ExceptionHandler;
using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

using System.Collections.Generic;
using System.Linq;


namespace CoffeeShop.Services
{
    public interface IApplicationPermissionService
    {
        ApplicationPermission GetDetail(string id);

        IEnumerable<ApplicationPermission> GetAll(int page, int pageSize, out int totalRow, string filter);

        IEnumerable<ApplicationPermission> GetAll();

        ApplicationPermission Add(ApplicationPermission appPermission);

        void Update(ApplicationPermission appPermission);

        bool Delete(string id);

        bool AddPermissionToUsers(IEnumerable<ApplicationUserPermission> permissions, string userId);

        List<ApplicationPermission> GetListPermissionByUserId(string userId);

        List<ApplicationPermission> GetListPermissionByUserName(string userName);

        List<ApplicationUser> GetListUserByPermissionId(string permissionId);

        List<ApplicationPermission> GetListPermissionByRoleName(string roleName);
        List<ApplicationPermission> GetListPermissionByRoleId(string roleId);

        void SaveChanges();
    }

    public class ApplicationPermissionService : IApplicationPermissionService
    {
        private IApplicationPermissionRepository _appPermissionRepository;
        private IUnitOfWork _unitOfWork;
        private IApplicationUserPermissionRepository _appUserPermissionRepository;

        public ApplicationPermissionService(IUnitOfWork unitOfWork,
            IApplicationUserPermissionRepository appUserPermissionRepository,
            IApplicationPermissionRepository appPermissionRepository)
        {
            this._appPermissionRepository = appPermissionRepository;
            this._appUserPermissionRepository = appUserPermissionRepository;
            this._unitOfWork = unitOfWork;
        }

        public ApplicationPermission Add(ApplicationPermission appPermission)
        {
            return _appPermissionRepository.CreateNewPermission(appPermission);
        }

        public bool Delete(string id)
        {
            var result = _appPermissionRepository.DeletePermission(id);
            return result;
        }

        public IEnumerable<ApplicationPermission> GetAll()
        {
            return _appPermissionRepository.GetAll();
        }

        public IEnumerable<ApplicationPermission> GetAll(int page, int pageSize, out int totalRow, string filter = null)
        {
            var query = _appPermissionRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.Name.Contains(filter));

            totalRow = query.Count();
            return query.OrderBy(x => x.Name).Skip(page * pageSize).Take(pageSize);
        }

        public ApplicationPermission GetDetail(string id)
        {
            return _appPermissionRepository.GetById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(ApplicationPermission appPermission)
        {
            if (_appPermissionRepository
                .CheckContains(x => x.Name == appPermission.Name && x.Id != appPermission.Id))
                throw new NameDuplicatedException("Tên không được trùng");
            _appPermissionRepository.Update(appPermission);
        }

        public bool AddPermissionToUsers(IEnumerable<ApplicationUserPermission> permissions, string userId)
        {
            foreach (var permission in permissions)
            {
                _appUserPermissionRepository.Add(permission);
            }
            return true;
        }

        public List<ApplicationPermission> GetListPermissionByUserId(string userId)
        {
            return _appPermissionRepository.GetListPermissionByUserId(userId);
        }

        public List<ApplicationUser> GetListUserByPermissionId(string permissionId)
        {
            return _appPermissionRepository.GetListUserByPermissionId(permissionId);
        }

        public List<ApplicationPermission> GetListPermissionByUserName(string userName)
        {
            return _appPermissionRepository.GetListPermissionByUserName(userName);
        }

        public List<ApplicationPermission> GetListPermissionByRoleName(string roleName)
        {
            return _appPermissionRepository.GetListPermissionByRoleName(roleName);
        }

        public List<ApplicationPermission> GetListPermissionByRoleId(string roleId)
        {
            return _appPermissionRepository.GetListPermissionByRoleId(roleId);
        }
    }
}