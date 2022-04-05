using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public class SystemConfigService : ISystemConfigService
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public ISystemConfigRepository _systemConfigRepository { get; set; }

        public SystemConfigService(IUnitOfWork unitOfWork, ISystemConfigRepository systemConfigRepository)
        {
            _unitOfWork = unitOfWork;
            _systemConfigRepository = systemConfigRepository;
        }

        public void Add(SystemConfig systemConfig)
        {
            _systemConfigRepository.Add(systemConfig);
        }

        public void Delete(SystemConfig systemConfig)
        {
            _systemConfigRepository.Delete(systemConfig);
        }

        public void Delete(int id)
        {
            _systemConfigRepository.Delete(id);
        }

        public IEnumerable<SystemConfig> GetAll()
        {
            return _systemConfigRepository.GetAll();
        }

        public IEnumerable<SystemConfig> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            //TODO: Select all SystemConfig by tag
            return _systemConfigRepository.GetMultiPaging(null, out totalRow, page, pageSize);
        }

        public IEnumerable<SystemConfig> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _systemConfigRepository.GetMultiPaging(null, out totalRow, page, pageSize);
        }

        public SystemConfig GetById(int id)
        {
            return _systemConfigRepository.GetById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(SystemConfig systemConfig)
        {
            _systemConfigRepository.Update(systemConfig);
        }
    }
}