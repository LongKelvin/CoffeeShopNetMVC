using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models.Models
{
    [Table("SupportOnlines")]
    public class SupportOnline : BaseEntity
    {
        [MaxLength(250)]
        [StringLength(250)]
        [Display(Name = "Name")]
        public string Name { get; set; } // nvarchar(250), null

        [MaxLength(250)]
        [StringLength(250)]
        [Display(Name = "Skype")]
        public string Skype { get; set; } // nvarchar(250), null

        [MaxLength(250)]
        [StringLength(250)]
        [Display(Name = "Facebook")]
        public string Facebook { get; set; } // nvarchar(250), null

        [MaxLength(250)]
        [StringLength(250)]
        [Display(Name = "Email")]
        public string Email { get; set; } // nvarchar(250), null

        [MaxLength(250)]
        [StringLength(250)]
        [Display(Name = "Zalo")]
        public string Zalo { get; set; } // nvarchar(250), null

        [Display(Name = "Status")]
        public bool? Status { get; set; } // bit, null

        [MaxLength(250)]
        [StringLength(250)]
        [Display(Name = "Department")]
        public string Department { get; set; } // nvarchar(250), null
    }
}