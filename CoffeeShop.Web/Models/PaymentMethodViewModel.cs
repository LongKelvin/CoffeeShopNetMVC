using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Web.Models
{
    public class PaymentMethodViewModel : ViewModelBase
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