using CoffeeShop.Models.Abstract;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models.Models
{
    [Table("ProductCategories")]
    public class ProductCategory : BaseEntity, IAuditable, ISeoable, ISwitchable
    {
        public ProductCategory()
        {
            this.Products = new HashSet<Product>();
        }

        [MaxLength(250)]
        [StringLength(250)]
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; } // nvarchar(250), not null

        [MaxLength(250)]
        [StringLength(250)]
        [Required(ErrorMessage = "Alias is required")]
        [Display(Name = "Alias")]
        public string Alias { get; set; } // varchar(250), not null

        [Display(Name = "Parent ID")]
        public int? ParentID { get; set; } // int, null

        [Display(Name = "Display Order")]
        public int? DisplayOrder { get; set; } // int, null

        [MaxLength(250)]
        [StringLength(250)]
        [Display(Name = "Meta Keyword")]
        public string MetaKeyword { get; set; } // nvarchar(250), null

        [MaxLength(250)]
        [StringLength(250)]
        [Display(Name = "Meta Description")]
        public string MetaDescription { get; set; } // nvarchar(250), null

        [Display(Name = "Create Date")]
        public DateTime? CreateDate { get; set; } // datetime, null

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Create By")]
        public string CreateBy { get; set; } // nvarchar(50), null

        [Display(Name = "Update Date")]
        public DateTime? UpdateDate { get; set; } // datetime, null

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Update By")]
        public string UpdateBy { get; set; } // nvarchar(50), null

        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public bool Status { get; set; } // bit, not null

        [Display(Name = "Home Flag")]
        public bool? HomeFlag { get; set; } // bit, null

        [MaxLength(500)]
        [StringLength(500)]
        [Display(Name = "Images")]
        public string Images { get; set; } // nvarchar(500), null

        [MaxLength(500)]
        [StringLength(500)]
        [Display(Name = "Description")]
        public string Description { get; set; } // nvarchar(500), null

        public virtual ICollection<Product> Products { get; set; }
    }
}