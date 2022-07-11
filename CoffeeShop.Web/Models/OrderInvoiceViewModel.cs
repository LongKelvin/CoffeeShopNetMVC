using System;

namespace CoffeeShop.Web.Models
{
    public class OrderInvoiceViewModel : ViewModelBase
    {
        public string InvoiceCode { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Cashier { get; set; }

        public int OrderId { get; set; }

        public virtual OrderViewModel Order { get; set; }

        public bool Status { get; set; }
    }
}