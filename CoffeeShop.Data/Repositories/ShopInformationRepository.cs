using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

namespace CoffeeShop.Data.Repositories
{
    public class ShopInformationRepository : RepositoryBase<ShopInformation>, IShopInfoRepository
    {
        public ShopInformationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}