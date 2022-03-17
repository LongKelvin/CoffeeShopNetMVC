using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

namespace CoffeeShop.Data.Repositories
{
    public class ErrorRepository : RepositoryBase<Error>, IErrorRepository
    {
        public ErrorRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}