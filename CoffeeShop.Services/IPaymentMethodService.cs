using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public interface IPaymentMethodService
    {
        PaymentMethod Add(PaymentMethod paymentMethod);

        PaymentMethod Update(PaymentMethod paymentMethod);

        bool Delete(PaymentMethod paymentMethod);

        bool Delete(int id);

        PaymentMethod GetById(int id);

        PaymentMethod GetByCode(int code);

        List<PaymentMethod> GetAll();
    }
}