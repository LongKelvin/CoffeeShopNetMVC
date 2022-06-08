using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

namespace CoffeeShop.Data.Repositories
{
    public interface IErrorRepository : IRepository<Error>
    {
        Error Create(Error error);
    }
}