using CoffeeShop.Models.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Web.Models
{
    public class PostCategoryViewModel : ViewModelBase
    {
        public string Alias { get; set; } // varchar(250), not null

        [Display(Name = "Parent ID")]
        public int? ParentID { get; set; } // int, null

        [Display(Name = "Display Order")]
        public int? DisplayOrder { get; set; } // int, null

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
        public string UpdatedBy { get; set; } // nvarchar(50), null

        [Required(ErrorMessage = "Status is required")]
        public bool Status { get; set; } // bit, not null

        [Display(Name = "Home Flag")]
        public bool? HomeFlag { get; set; } // bit, null

      
        [Display(Name = "Images")]
        public string Images { get; set; } // nvarchar(500), null

        [Display(Name = "Description")]
        public string Description { get; set; } // nvarchar(500), null

        
        [Display(Name = "Name")]
        public string Name { get; set; } // nvarchar(250), not null

        public virtual ICollection<PostViewModel> Posts { get; set; }
    }
}