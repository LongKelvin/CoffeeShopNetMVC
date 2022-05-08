using CoffeeShop.Models.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Services
{
    public interface IShopInfoService
    {
        ShopInformation GetShopInfo(int id=1);
    }
}
