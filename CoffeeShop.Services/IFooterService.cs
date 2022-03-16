using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public interface IFooterService
    {
        void Add(Footer footer);

        void Update(Footer footer);

        void Delete(Footer footer);

        void Delete(int id);

        IEnumerable<Footer> GetAll();

        IEnumerable<Footer> GetAllPaging(int page, int pageSize, out int totalRow);

        IEnumerable<Footer> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        Footer GetById(int id);

        void SaveChanges();
    }
}