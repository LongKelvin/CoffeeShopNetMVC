using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

using System;

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
            return _errorRepository.Create(error);
        }

        //public void Save()
        //{
        //    _unitOfWork.Commit();
        //}

        public void LogError(Exception ex)
        {
            try
            {
                Error error = new Error
                {
                    CreatedDate = DateTime.Now,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                };

                _errorRepository.Create(error);
            }
            catch
            {
                throw;
            }
        }
    }
}