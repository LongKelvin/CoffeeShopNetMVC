using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models.Models
{
    [Table("PaymentMethod")]
    public class PaymentMethod : BaseEntity
    {
        [Required]
        public string PaymentName { get; set; }

        [Required]
        public int PaymentCode { get; set; }

        [Required]
        public bool Status { get; set; }

        public string LogoImage { get; set; }
    }
}