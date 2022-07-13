using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Common
{
    public static class OrderInvoiceHelper
    {
        public static string GenerateInvoiceCode(string orderId)
        {
            return "HD" + orderId + DateTime.Now.Ticks;
        }
    }
}
