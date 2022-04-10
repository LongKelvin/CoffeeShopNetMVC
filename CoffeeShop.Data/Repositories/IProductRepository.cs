using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

namespace CoffeeShop.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        void Detach(Product product);

        void MakeAsModified(Product entity);

        CoffeeShopDbContext DbContext { get; }
    }
}