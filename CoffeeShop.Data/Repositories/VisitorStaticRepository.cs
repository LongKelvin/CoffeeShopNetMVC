using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

namespace CoffeeShop.Data.Repositories
{
    public class VisitorStaticRepository : RepositoryBase<VisitorStatistic>, IVisitorStaticRepository
    {
        public VisitorStaticRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}