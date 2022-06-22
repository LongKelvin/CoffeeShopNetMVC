using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Web.Models
{
    public class SlideViewModel : ViewModelBase
    {
        [MaxLength(250)]
        [StringLength(250)]
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Title")]
        public string Title { get; set; } // nvarchar(250), not null

        [MaxLength(50)]
        [StringLength(50)]
        [Required(ErrorMessage = "Action Name is required")]
        [Display(Name = "Action Name")]
        public string ActionName { get; set; } // nvarchar(250), not null

        [MaxLength(500)]
        [StringLength(500)]
        [Required(ErrorMessage = "Images is required")]
        [Display(Name = "Images")]
        public string Images { get; set; } // nvarchar(500), not null

        [MaxLength(250)]
        [StringLength(250)]
        [Display(Name = "Description")]
        public string Description { get; set; } // nvarchar(250), null

        [MaxLength(500)]
        [StringLength(500)]
        [Required(ErrorMessage = "URL is required")]
        [Display(Name = "URL")]
        public string URL { get; set; } // nvarchar(500), not null

        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public bool Status { get; set; } // bit, not null

        [Display(Name = "Display Order")]
        [Required]
        public int DisplayOrder { get; set; } // int, null
    }
}