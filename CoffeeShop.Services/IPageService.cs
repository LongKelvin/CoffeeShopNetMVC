using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Services
{
    public interface IPageService
    {
        void Add(Page page);
        void Update(Page page);
        void Delete(Page page);
        void Delete(int id);
        IEnumerable<Page> GetAll();
        IEnumerable<Page> GetAllPaging(int page, int pageSize, out int totalRow);
        IEnumerable<Page> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);
        Page GetById(int id);
        void SaveChanges();
    }
}
