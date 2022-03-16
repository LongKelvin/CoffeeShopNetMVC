using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public interface IVisitorStatisticService
    {
        void Add(VisitorStatistic visitorStatistic);

        void Update(VisitorStatistic visitorStatistic);

        void Delete(VisitorStatistic visitorStatistic);

        void Delete(int id);

        IEnumerable<VisitorStatistic> GetAll();

        IEnumerable<VisitorStatistic> GetAllPaging(int page, int pageSize, out int totalRow);

        IEnumerable<VisitorStatistic> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        VisitorStatistic GetById(int id);

        void SaveChanges();
    }
}