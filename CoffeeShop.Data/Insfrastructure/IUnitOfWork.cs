namespace CoffeeShop.Data.Insfrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}