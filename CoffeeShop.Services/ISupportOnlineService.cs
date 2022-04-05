using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public interface ISupportOnlineService
    {
        void Add(SupportOnline supportOnline);

        void Update(SupportOnline supportOnline);

        void Delete(SupportOnline supportOnline);

        void Delete(int id);

        IEnumerable<SupportOnline> GetAll();

        IEnumerable<SupportOnline> GetAllPaging(int page, int pageSize, out int totalRow);

        IEnumerable<SupportOnline> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        SupportOnline GetById(int id);

        void SaveChanges();
    }
}