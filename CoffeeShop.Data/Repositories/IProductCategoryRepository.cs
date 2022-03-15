using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Data.Repositories
{
    public interface IProductCategoryRepository
    {
        IEnumerable<ProductCategory> GetByAlias(string alias);
    }
}