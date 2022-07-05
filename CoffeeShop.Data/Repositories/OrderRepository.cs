using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

using System.Data.Entity;
using System.Linq;

namespace CoffeeShop.Data.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        bool IOrderRepository.Delete(int id)
        {
            var oderById = DbContext.Orders.Where(x => x.ID == id).FirstOrDefault();
            oderById.Status = false;
            return true;
        }
    }
}