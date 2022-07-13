using CoffeeShop.Models.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShop.Services
{
    public interface IOrderService
    {
        Order Add(Order order);

        Order Update(Order order);

        Order Delete(Order order);

        bool Delete(int id);

        IEnumerable<Order> GetAll();

        IEnumerable<Order> GetAllPaging(int page, int pageSize, out int totalRow);

        IEnumerable<Order> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        Order GetById(int id);

        Order GetById(int id, string[] includes);

        bool UpdatePaymentStatus(int orderID, int value);

        void SaveChanges();

        Task SaveChangesAsync();

        Task<List<Order>> GetAllAsync();

        bool UpdateOrderStatus(int orderId, int orderStatus);

        bool CancelOrder(int orderId, string note);


    }
}