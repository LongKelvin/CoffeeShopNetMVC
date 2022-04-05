using System;

namespace CoffeeShop.Data.Insfrastructure
{
    public interface IDbFactory : IDisposable //factory design pattern
    {
        CoffeeShopDbContext Init();
    }
}