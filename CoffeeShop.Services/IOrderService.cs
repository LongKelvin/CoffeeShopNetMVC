using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public interface IOrderService
    {
        Order Add(Order order);

        Order Update(Order order);

        Order Delete(Order order);

        Order Delete(int id);

        IEnumerable<Order> GetAll();

        IEnumerable<Order> GetAllPaging(int page, int pageSize, out int totalRow);

        IEnumerable<Order> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        Order GetById(int id);

        void SaveChanges();
    }
}