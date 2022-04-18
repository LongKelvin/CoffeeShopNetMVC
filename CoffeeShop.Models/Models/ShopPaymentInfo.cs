using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Models.Models
{
    [Table("ShopPayment")]
    public class ShopPaymentInfo : BaseEntity
    {
        [Required]
        public string BankName { get; set; }

        public string BankType { get; set; }

        [Required]
        public string CardNumner { get; set; }

        public int CardNumLimit { get; set; }

        [Required]
        public string AccountName { get; set; }

        public string CardPhone { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
