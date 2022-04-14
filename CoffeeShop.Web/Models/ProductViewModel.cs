using CoffeeShop.Models.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Web.Models
{
    public class ProductViewModel : ViewModelBase
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
        public string UpdatedBy { get; set; } // nvarchar(50), null

      
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

        [Required(ErrorMessage = "Price is required")]
        [Display(Name = "Price")]
        public decimal Price { get; set; } // decimal(18,2), not null

        [Display(Name = "Promotion Price")]
        public decimal? PromotionPrice { get; set; } // decimal(18,2), null

        [Display(Name = "Warranty")]
        public int? Warranty { get; set; } // int, null

        [Display(Name = "Mfg Date")]
        public DateTime? ManufacturingDate { get; set; }

        [Display(Name = "Exp Date")]
        public DateTime? ExpireDate { get; set; }

        [Display(Name = "Tags")]
        public string TagsString { get; set; }

        public virtual ProductCategoryViewModel ProductCategory { get; set; }

        public virtual ICollection<OrderDetailViewModel> OrderDetails { get; set; }
        public virtual ICollection<TagViewModel> Tags { get; set; }
    }
}