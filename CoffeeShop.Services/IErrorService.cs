using CoffeeShop.Models.Models;

namespace CoffeeShop.Services
{
    public interface IErrorService
    {
        Error CreateError(Error error);

        void Save();
    }
}