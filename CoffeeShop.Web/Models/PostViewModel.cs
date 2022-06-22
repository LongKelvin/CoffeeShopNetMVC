using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Web.Models
{
    public class PostViewModel : ViewModelBase
    {
        [Display(Name = "Name")]
        public string Name { get; set; } // nvarchar(250), not null

        [Display(Name = "Alias")]
        public string Alias { get; set; } // varchar(250), not null

        [Display(Name = "Meta Keyword")]
        public string MetaKeyword { get; set; } // nvarchar(250), null

        [Display(Name = "Meta Description")]
        public string MetaDescription { get; set; } // nvarchar(250), null

        [Display(Name = "Create Date")]
        public DateTime? CreatedDate { get; set; } // datetime, null

        [Display(Name = "Create By")]
        public string CreatedBy { get; set; } // nvarchar(50), null

        [Display(Name = "Update Date")]
        public DateTime? UpdatedDate { get; set; } // datetime, null

        [Display(Name = "Update By")]
        public string UpdatedBy { get; set; } // nvarchar(50), not null

        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public bool Status { get; set; } // bit, not null

        [Display(Name = "Home Flag")]
        public bool HomeFlag { get; set; } // bit, not null

        [Display(Name = "Images")]
        public string Images { get; set; } // nvarchar(500), null

        [Display(Name = "Category ID")]
        public int? CategoryID { get; set; } // int, null

        [Display(Name = "Hot Flag")]
        public bool? HotFlag { get; set; } // bit, null

        [Display(Name = "View Count")]
        public int? ViewCount { get; set; } // int, null

        [Display(Name = "More Images")]
        public string MoreImages { get; set; } // XML(.), null

        [Display(Name = "Description")]
        public string Description { get; set; } // nvarchar(500), null

        [Display(Name = "Content")]
        public string Content { get; set; } // nvarchar(max), null

        public virtual PostCategoryViewModel PostCategory { get; set; }

        public virtual ICollection<TagViewModel> Tags { get; set; }
    }
}