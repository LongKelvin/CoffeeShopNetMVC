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

        public IOrderDetailRepository _orderDetailRepository { get; set; }

        public OrderService(IUnitOfWork unitOfWork, IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public Order Add(Order order)
        {
            var listOrderDetail = order.OrderDetails;

            order.OrderDetails = null;

            var orderResult = _orderRepository.Add(order);
            _unitOfWork.Commit();

            orderResult.OrderDetails = listOrderDetail;
            _orderRepository.Update(orderResult);
            _unitOfWork.Commit();

            return orderResult;
        }

        public Order Delete(Order order)
        {
            return _orderRepository.Delete(order);
        }

        public Order Delete(int id)
        {
            return _orderRepository.Delete(id);
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

        public Order Update(Order order)
        {
            _orderRepository.Update(order);
            return order;
        }
    }
}