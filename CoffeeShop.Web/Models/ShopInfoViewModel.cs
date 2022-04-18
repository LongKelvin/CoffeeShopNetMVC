using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CoffeeShop.Web.Models
{
    public class ShopInfoViewModel : ViewModelBase
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; } // nvarchar(250), not null

        [Display(Name = "Logo")]
        public string Logo { get; set; } // nvarchar(250), not null

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; } // nvarchar(250), null

        [Display(Name = "Address 2")]
        public string Address2 { get; set; } // nvarchar(250), null

        [Required]
        [Display(Name = "Telephone")]
        public string Telephone { get; set; } // nvarchar(250), null

        [Display(Name = "Mobiphone 1")]
        public string Mobiphone1 { get; set; } // nvarchar(250), null

        [Display(Name = "Mobiphone 2")]
        public string Mobiphone2 { get; set; } // nvarchar(250), null

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email ")]
        public string Email { get; set; } // nvarchar(250), null

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Support Email ")]
        public string Email2 { get; set; } // nvarchar(250), null

        [Required]
        public bool Status { get; set; }
    }
}