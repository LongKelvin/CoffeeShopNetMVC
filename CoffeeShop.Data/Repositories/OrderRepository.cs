using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

namespace CoffeeShop.Data.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}