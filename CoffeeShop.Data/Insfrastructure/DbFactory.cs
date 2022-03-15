using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Data.Insfrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        CoffeeShopDbContext _dbContext { get; set; }
        public CoffeeShopDbContext Init()
        {
            return _dbContext??(_dbContext = new CoffeeShopDbContext());
        }

        protected override void DisposeCore()
        {
            if(_dbContext != null)
                _dbContext.Dispose();
        }
    }
}
