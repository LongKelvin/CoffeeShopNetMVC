using CoffeeShop.Models.Models;

using System;

namespace CoffeeShop.Services
{
    public interface IErrorService
    {
        Error CreateError(Error error);

        void LogError(Exception ex);

        //void Save();
    }
}