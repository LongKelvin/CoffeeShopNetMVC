using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public IOrderDetailRepository _orderDetailRepository { get; set; }

        public OrderDetailService(IUnitOfWork unitOfWork, IOrderDetailRepository orderDetailRepository)
        {
            _unitOfWork = unitOfWork;
            _orderDetailRepository = orderDetailRepository;
        }

        public void Add(OrderDetail orderDetail)
        {
            _orderDetailRepository.Add(orderDetail);
        }

        public void Delete(OrderDetail orderDetail)
        {
            _orderDetailRepository.Delete(orderDetail);
        }

        public void Delete(int id)
        {
            _orderDetailRepository.Delete(id);
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return _orderDetailRepository.GetAll();
        }

        public IEnumerable<OrderDetail> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            //TODO: Select all OrderDetail by tag
            return _orderDetailRepository.GetMultiPaging(null, out totalRow, page, pageSize);
        }

        public IEnumerable<OrderDetail> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _orderDetailRepository.GetMultiPaging(null, out totalRow, page, pageSize);
        }

        public OrderDetail GetById(int id)
        {
            return _orderDetailRepository.GetById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(OrderDetail orderDetail)
        {
            _orderDetailRepository.Update(orderDetail);
        }
    }
}