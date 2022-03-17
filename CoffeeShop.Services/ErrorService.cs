using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

namespace CoffeeShop.Services
{
    public class ErrorService : IErrorService
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public IErrorRepository _errorRepository { get; set; }

        public ErrorService(IUnitOfWork unitOfWork, IErrorRepository errorRepository)
        {
            _unitOfWork = unitOfWork;
            _errorRepository = errorRepository;
        }

        public Error CreateError(Error error)
        {
            return _errorRepository.Add(error);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}