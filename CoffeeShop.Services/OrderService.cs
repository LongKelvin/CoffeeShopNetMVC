using CoffeeShop.Common;
using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

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

        public bool Delete(int id)
        {
            return _orderRepository.Delete(id);
        }

        public IEnumerable<Order> GetAll()
        {
            return _orderRepository.GetMulti(x => x.Status == true);
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

        public bool UpdatePaymentStatus(int orderID, int value)
        {
            var updateOrder = _orderRepository.GetById(orderID);
            updateOrder.PaymentStatus = value;
            return updateOrder.ID > 0;
        }

        public Order GetById(int id, string[] includes)
        {
            return _orderRepository.GetByCondition(x => x.ID == id, includes);
        }

        public async Task SaveChangesAsync()
        {
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public bool UpdateOrderStatus(int orderId, int orderStatus)
        {
            var order = _orderRepository.GetById(orderId);
            switch (orderStatus)
            {
                case (int)CommonConstants.OrderStatus.Pending:
                case (int)CommonConstants.OrderStatus.Confirmed:
                case (int)CommonConstants.OrderStatus.Processing:
                    order.OrderStatus = orderStatus;
                    break;

                case (int)CommonConstants.OrderStatus.Shipping:
                    {
                        order.ShippingStatus = (int)CommonConstants.ShippingStatus.Shipping;
                        order.OrderStatus = orderStatus;
                    }
                    break;

                case (int)CommonConstants.OrderStatus.Complete:

                    {
                        order.ShippingStatus = (int)CommonConstants.ShippingStatus.Delivered;
                        order.PaymentStatus = (int)CommonConstants.PaymentStatus.Paid;
                        order.OrderStatus = orderStatus;
                    }
                    break;

                case (int)CommonConstants.OrderStatus.Canceled:

                    {
                        if (order.PaymentStatus == (int)CommonConstants.PaymentStatus.Paid)
                        {
                            order.PaymentStatus = (int)CommonConstants.PaymentStatus.Refunded;
                        }
                        else
                        {
                            order.PaymentStatus = (int)CommonConstants.PaymentStatus.Canceled;
                        }

                        order.ShippingStatus = (int)CommonConstants.ShippingStatus.Canceled;
                        order.OrderStatus = orderStatus;
                    }
                    break;

                case (int)CommonConstants.OrderStatus.Refunded:

                    {
                        if (order.PaymentStatus == (int)CommonConstants.PaymentStatus.Paid)
                        {
                            order.PaymentStatus = (int)CommonConstants.PaymentStatus.Refunded;
                        }

                        order.ShippingStatus = (int)CommonConstants.ShippingStatus.Canceled;
                        order.OrderStatus = orderStatus;
                    }
                    break;

                case (int)CommonConstants.OrderStatus.Falied:

                    {
                        if (order.PaymentStatus == (int)CommonConstants.PaymentStatus.Paid)
                        {
                            order.PaymentStatus = (int)CommonConstants.PaymentStatus.Refunded;
                        }
                        else
                        {
                            order.PaymentStatus = (int)CommonConstants.PaymentStatus.Canceled;
                        }

                        order.ShippingStatus = (int)CommonConstants.ShippingStatus.Canceled;
                        order.OrderStatus = orderStatus;
                    }
                    break;

                default: return false;
            }

            _orderRepository.Update(order);
            return _orderRepository.GetById(orderId).ID > 0;
        }

        public bool CancelOrder(int orderId, string note)
        {
            var order = _orderRepository.GetById(orderId);
            if (order == null)
                return false;

            if (order.PaymentStatus == (int)CommonConstants.PaymentStatus.Paid)
            {
                order.PaymentStatus = (int)CommonConstants.PaymentStatus.Refunded;
            }
            else
            {
                order.PaymentStatus = (int)CommonConstants.PaymentStatus.Canceled;
            }

            order.ShippingStatus = (int)CommonConstants.ShippingStatus.Canceled;
            order.OrderStatus = (int)CommonConstants.OrderStatus.Canceled;

            _orderRepository.Update(order);
            return _orderRepository.GetById(orderId).ID > 0;
        }
    }
}