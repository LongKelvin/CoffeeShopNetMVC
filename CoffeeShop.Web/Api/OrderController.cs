using AutoMapper;

using CoffeeShop.Common;
using CoffeeShop.Models.Models;
using CoffeeShop.Services;
using CoffeeShop.Web.Infrastucture.Core;
using CoffeeShop.Web.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace CoffeeShop.Web.Api
{
    [RoutePrefix(CommonConstants.API_Order)]
    public class OrderController : ApiControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IErrorService errorService, IOrderService orderService) : base(errorService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("GetAll")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyWord = null, string orderStatus = null, int page = 0, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                var listOrders = _orderService.GetAll();
                //Order by
                IEnumerable<Order> query = listOrders.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var listOrderVM = Mapper.Map<List<OrderViewModel>>(query);

                totalRow = listOrderVM.Count;

                var resultSet = new PaginationSet<OrderViewModel>
                {
                    Items = listOrderVM,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                    Page = page
                };

                return request.CreateResponse(HttpStatusCode.OK, resultSet);
            });
        }

        [HttpGet]
        [Route("GetListOrderStatus")]
        public HttpResponseMessage GetListOrderStatus(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                List<OrderStatus> listOrderStatus = new List<OrderStatus>();
                foreach (CommonConstants.OrderStatus status in (CommonConstants.OrderStatus[])Enum.GetValues(typeof(CommonConstants.OrderStatus)))
                {
                    if ((int)status == (int)CommonConstants.OrderStatus.Canceled)
                        continue;

                    listOrderStatus.Add(new OrderStatus
                    {
                        ID = (int)status,
                        StatusCode = (int)status,
                        StatusName = status.ToString(),
                        StatusDescription = "Default status " + status.ToString(),
                        IsActive = true,
                        IsCanDelete = false,
                    });
                }
                return request.CreateResponse(HttpStatusCode.OK, listOrderStatus);
            });
        }

        [HttpGet]
        [Route("GetOrderStatus/{id:int}")]
        public HttpResponseMessage GetOrderStatusById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var orderById = _orderService.GetById(id);

                if (orderById == null)
                    return request.CreateResponse(HttpStatusCode.BadRequest, "Order not found");

                var orderStatusViewModel = new OrderStatusUpdateViewModel
                {
                    OrderId = orderById.ID,
                    OrderStatus = orderById.OrderStatus,
                    PaymentStatus = orderById.PaymentStatus,
                    ShippingStatus = orderById.ShippingStatus
                };

                return request.CreateResponse(HttpStatusCode.OK, orderStatusViewModel);
            });
        }

        [HttpPost]
        [Route("UpdateOrderStatus")]
        public HttpResponseMessage UpdateOrderStatus(HttpRequestMessage request, OrderStatusUpdateViewModel updateVM)
        {
            return CreateHttpResponse(request, () =>
            {
                var orderById = _orderService.GetById(updateVM.OrderId);

                if (orderById == null)
                    return request.CreateResponse(HttpStatusCode.BadRequest, "Order not found");

                bool result = _orderService.UpdateOrderStatus(orderById.ID, updateVM.OrderStatus);
                _orderService.SaveChanges();
                return request.CreateResponse(HttpStatusCode.OK, result);
            });
        }

        [HttpPost]
        [Route("ConfirmOrder/{orderId:int}")]
        public HttpResponseMessage ConfirmOrder(HttpRequestMessage request, int orderId)
        {
            return CreateHttpResponse(request, () =>
            {
                var orderById = _orderService.GetById(orderId);

                if (orderById == null)
                    return request.CreateResponse(HttpStatusCode.BadRequest, "Order not found");

                var updateVM = new OrderStatusUpdateViewModel
                {
                    OrderId = orderId,
                    OrderStatus = (int)CommonConstants.OrderStatus.Confirmed
                };

                bool result = _orderService.UpdateOrderStatus(orderById.ID, updateVM.OrderStatus);
                _orderService.SaveChanges();
                return request.CreateResponse(HttpStatusCode.OK, result);
            });
        }

        [HttpPost]
        [Route("CancelOrder")]
        public HttpResponseMessage CancelOrder(HttpRequestMessage request, OrderStatusUpdateViewModel updateVM)
        {
            return CreateHttpResponse(request, () =>
            {
                var orderById = _orderService.GetById(updateVM.OrderId);

                if (orderById == null)
                    return request.CreateResponse(HttpStatusCode.BadRequest, "Order not found");

                updateVM.OrderStatus = (int)CommonConstants.OrderStatus.Canceled;

                bool result = _orderService.CancelOrder(orderById.ID, updateVM.Note);
                _orderService.SaveChanges();
                return request.CreateResponse(HttpStatusCode.OK, result);
            });
        }

        [HttpDelete]
        [Route("DeleteOrder/{id:int}")]
        public HttpResponseMessage CancelOrder(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var orderById = _orderService.GetById(id);

                if (orderById == null)
                    return request.CreateResponse(HttpStatusCode.BadRequest, "Order not found");

                bool result = _orderService.Delete(orderById.ID);
                _orderService.SaveChanges();
                return request.CreateResponse(HttpStatusCode.OK, result);
            });
        }

        [HttpDelete]
        [Route("DeleteMultiIOrder")]
        public HttpResponseMessage DeleteMultiOrder(HttpRequestMessage request, string ids)
        {
            return CreateHttpResponse(request, () =>
            {
                try
                {
                    var listProduct = new JavaScriptSerializer().Deserialize<List<int>>(ids);
                    foreach (var item in listProduct)
                    {
                        _orderService.Delete(item);
                    }

                    _orderService.SaveChanges();
                }
                catch
                {
                    throw;
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [HttpGet]
        [Route("GetOrderDetail/{id:int}")]
        public HttpResponseMessage GetOrderDetail(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                if (id == 0 || id < 0)
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, "ID is invalid");
                }

                var orderById = _orderService.GetById(id, new string[] { "OrderDetails" });

                if (orderById == null)
                    return request.CreateResponse(HttpStatusCode.BadRequest, "Order not found");

                return request.CreateResponse(HttpStatusCode.OK, orderById);
            });
        }
    }
}