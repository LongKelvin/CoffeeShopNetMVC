namespace CoffeeShop.Data.Insfrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private CoffeeShopDbContext _dbContext { get; set; }

        public CoffeeShopDbContext Init()
        {
            return _dbContext ?? (_dbContext = new CoffeeShopDbContext());
        }

        protected override void DisposeCore()
        {
            if (_dbContext != null)
                _dbContext.Dispose();
        }
    }
}