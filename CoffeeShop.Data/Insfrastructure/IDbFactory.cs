using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Data.Insfrastructure
{
    public interface IDbFactory: IDisposable //factory design pattern
    {
        CoffeeShopDbContext Init();
    }
}
