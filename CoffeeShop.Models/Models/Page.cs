using CoffeeShop.Models.Abstract;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models.Models
{
    [Table("Pages")]
    public class Page : BaseEntity, ISeoable, ISwitchable
    {
        [MaxLength(250)]
        [StringLength(250)]
        [Display(Name = "Name")]
        public string Name { get; set; } // nvarchar(250), null

        [MaxLength]
        [Display(Name = "Content")]
        public string Content { get; set; } // nvarchar(max), null

        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public bool Status { get; set; } // bit, not null

        [MaxLength(250)]
        [StringLength(250)]
        [Display(Name = "Meta Keyword")]
        public string MetaKeyword { get; set; } // nvarchar(250), null

        [MaxLength(250)]
        [StringLength(250)]
        [Display(Name = "Meta Description")]
        public string MetaDescription { get; set; } // nvarchar(250), null
    }
}