using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

namespace CoffeeShop.Data.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        Tag GetByIdString(string id);
    }
}