using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

using System.Collections.Generic;
using System.Linq;

namespace CoffeeShop.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentMethodService(IPaymentMethodRepository paymentMethodRepository,
            IUnitOfWork unitOfWork)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _unitOfWork = unitOfWork;
        }

        public PaymentMethod Add(PaymentMethod paymentMethod)
        {
            return _paymentMethodRepository.Add(paymentMethod);
        }

        public bool Delete(PaymentMethod paymentMethod)
        {
            var result = _paymentMethodRepository.Delete(paymentMethod);
            return result != null;
        }

        public bool Delete(int id)
        {
            var result = _paymentMethodRepository.Delete(id);
            return result != null;
        }

        public List<PaymentMethod> GetAll()
        {
            return _paymentMethodRepository.GetAll().ToList();
        }

        public PaymentMethod GetByCode(int code)
        {
            return _paymentMethodRepository.GetByCondition(x => x.PaymentCode == code);
        }

        public PaymentMethod GetById(int id)
        {
            return _paymentMethodRepository.GetById(id);
        }

        public PaymentMethod Update(PaymentMethod paymentMethod)
        {
            _paymentMethodRepository.Update(paymentMethod);
            return _paymentMethodRepository.GetById(paymentMethod.ID);
        }
    }
}