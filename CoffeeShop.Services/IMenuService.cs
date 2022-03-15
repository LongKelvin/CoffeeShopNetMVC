using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Services
{
    public interface IMenuService
    {
        void Add(Menu menu);
        void Update(Menu menu);
        void Delete(Menu menu);
        void Delete(int id);
        IEnumerable<Menu> GetAll();
        IEnumerable<Menu> GetAllPaging(int page, int pageSize, out int totalRow);
        IEnumerable<Menu> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);
        Menu GetById(int id);
        void SaveChanges();
    }
}
