using System;

namespace CoffeeShop.Common.ExceptionHandler
{
    public class OrderQuantityNotValidException : Exception
    {
        public OrderQuantityNotValidException()
        {
        }

        public OrderQuantityNotValidException(string message)
        : base(message)
        {
        }

        public OrderQuantityNotValidException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}