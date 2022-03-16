using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public interface ISystemConfigService
    {
        void Add(SystemConfig systemConfig);

        void Update(SystemConfig systemConfig);

        void Delete(SystemConfig systemConfig);

        void Delete(int id);

        IEnumerable<SystemConfig> GetAll();

        IEnumerable<SystemConfig> GetAllPaging(int page, int pageSize, out int totalRow);

        IEnumerable<SystemConfig> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        SystemConfig GetById(int id);

        void SaveChanges();
    }
}