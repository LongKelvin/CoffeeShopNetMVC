using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

namespace CoffeeShop.Data.Repositories
{
    public class VisitorStatisticRepository : RepositoryBase<VisitorStatistic>, IVisitorStatisticRepository
    {
        public VisitorStatisticRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}