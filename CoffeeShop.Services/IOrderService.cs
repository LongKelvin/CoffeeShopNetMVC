using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Services
{
    public interface IOrderService
    {
        void Add(Order order);
        void Update(Order order);
        void Delete(Order order);
        void Delete(int id);
        IEnumerable<Order> GetAll();
        IEnumerable<Order> GetAllPaging(int page, int pageSize, out int totalRow);
        IEnumerable<Order> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);
        Order GetById(int id);
        void SaveChanges();
    }
}
