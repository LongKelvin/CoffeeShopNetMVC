using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public interface IOrderDetailService
    {
        void Add(OrderDetail orderDetail);

        void Update(OrderDetail orderDetail);

        void Delete(OrderDetail orderDetail);

        void Delete(int id);

        IEnumerable<OrderDetail> GetAll();

        IEnumerable<OrderDetail> GetAllPaging(int page, int pageSize, out int totalRow);

        IEnumerable<OrderDetail> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        OrderDetail GetById(int id);

        void SaveChanges();
    }
}