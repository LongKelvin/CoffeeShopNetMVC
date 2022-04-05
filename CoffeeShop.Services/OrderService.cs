using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public class OrderService : IOrderService
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public IOrderRepository _orderRepository { get; set; }

        public OrderService(IUnitOfWork unitOfWork, IOrderRepository orderRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
        }

        public void Add(Order order)
        {
            _orderRepository.Add(order);
        }

        public void Delete(Order order)
        {
            _orderRepository.Delete(order);
        }

        public void Delete(int id)
        {
            _orderRepository.Delete(id);
        }

        public IEnumerable<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public IEnumerable<Order> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            //TODO: Select all Order by tag
            return _orderRepository.GetMultiPaging(x => x.Status == true, out totalRow, page, pageSize);
        }

        public IEnumerable<Order> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _orderRepository.GetMultiPaging(x => x.Status == true, out totalRow, page, pageSize);
        }

        public Order GetById(int id)
        {
            return _orderRepository.GetById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Order order)
        {
            _orderRepository.Update(order);
        }
    }
}