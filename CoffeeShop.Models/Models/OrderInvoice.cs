using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Models.Models
{
    [Table("OrderInvoices")]
    public class OrderInvoice : BaseEntity
    {
        public string InvoiceCode { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Cashier { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        public bool Status { get; set; }
    }
}
