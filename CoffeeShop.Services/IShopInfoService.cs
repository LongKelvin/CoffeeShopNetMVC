using CoffeeShop.Models.Models;

namespace CoffeeShop.Services
{
    public interface IShopInfoService
    {
        ShopInformation GetShopInfo(int id = 1);
    }
}