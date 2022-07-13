using System.Threading.Tasks;

namespace CoffeeShop.Data.Insfrastructure
{
    public interface IUnitOfWork
    {
        void Commit();

        Task CommitAsync();
    }
}