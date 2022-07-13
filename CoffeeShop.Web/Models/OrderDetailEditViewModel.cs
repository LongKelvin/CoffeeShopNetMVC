using CoffeeShop.Models.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeShop.Web.Models
{
    public class OrderDetailEditViewModel
    {
        public Order Order { get; set; }

        public string PaymentStatus { get; set; }

        public string OrderStatus { get; set; }

        public string ShippingStatus { get; set; }

        public string PaymentMethod { get; set; }
    }
}