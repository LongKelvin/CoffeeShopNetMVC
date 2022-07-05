using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeShop.Web.Models
{
    public class OrderStatusUpdateViewModel
    {
        public int OrderId { get; set; }
        public int OrderStatus { get; set; }
        public int PaymentStatus { get; set; }
        public int ShippingStatus { get; set; }
        public string Note { get; set; }
    }
}