using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

using System;
using System.Linq.Expressions;

namespace CoffeeShop.Services
{
    public interface IOrderInvoiceService
    {
        void CreateInvoice(OrderInvoice invoice);

        OrderInvoice GetById(int id);

        OrderInvoice GetByCondition(Expression<Func<OrderInvoice, bool>> expression);
        void SaveChanges();
    }

    public class OrderInvoiceService : IOrderInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderInvoiceRepository _invoiceRepository;

        public OrderInvoiceService(IUnitOfWork unitOfWork, IOrderInvoiceRepository invoiceRepository)
        {
            _unitOfWork = unitOfWork;
            _invoiceRepository = invoiceRepository;
        }

        public void CreateInvoice(OrderInvoice invoice)
        {
            _invoiceRepository.Add(invoice);
        }

        public OrderInvoice GetByCondition(Expression<Func<OrderInvoice, bool>> expression)
        {
            return _invoiceRepository.GetByCondition(expression);
        }

        public OrderInvoice GetById(int id)
        {
            return _invoiceRepository.GetById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}