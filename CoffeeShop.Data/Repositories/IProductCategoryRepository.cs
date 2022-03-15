using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Data.Repositories
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        IEnumerable<ProductCategory> GetByAlias(string alias);
    }
}