using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

namespace CoffeeShop.Data.Repositories
{
    public class PageRepository : RepositoryBase<Page>, IPageRepository
    {
        public PageRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}