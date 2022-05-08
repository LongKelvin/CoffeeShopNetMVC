using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Data.Repositories
{
    public class ShopInformationRepository : RepositoryBase<ShopInformation>, IShopInfoRepository
    {
        public ShopInformationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

       
    }
}
