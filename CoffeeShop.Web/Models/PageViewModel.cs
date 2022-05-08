using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Web.Models
{
    public class PageViewModel : ViewModelBase
    {
        [Display(Name = "Name")]
        public string Name { get; set; } // nvarchar(250), null

        [Display(Name = "Alias")]
        public string Alias { get; set; } // nvarchar(250), null

        [Display(Name = "Content")]
        public string Content { get; set; } // nvarchar(max), null

        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public bool Status { get; set; } // bit, not null

        [Display(Name = "Meta Keyword")]
        public string MetaKeyword { get; set; } // nvarchar(250), null

        [Display(Name = "Meta Description")]
        public string MetaDescription { get; set; } // nvarchar(250), null
    }
}