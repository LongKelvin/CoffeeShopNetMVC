using CoffeeShop.Models.Abstract;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models.Models
{
    [Table("Products")]
    public class Product : BaseEntity, IAuditable, ISwitchable, ISeoable
    {
        public Product()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
            this.Tags = new HashSet<Tag>();
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

        [MaxLength(250)]
        [StringLength(250)]
        [Display(Name = "Meta Keyword")]
        public string MetaKeyword { get; set; } // nvarchar(250), null

        [MaxLength(250)]
        [StringLength(250)]
        [Display(Name = "Meta Description")]
        public string MetaDescription { get; set; } // nvarchar(250), null

        [Display(Name = "Create Date")]
        public DateTime? CreatedDate { get; set; } // datetime, null

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Create By")]
        public string CreatedBy { get; set; } // nvarchar(50), null

        [Display(Name = "Update Date")]
        public DateTime? UpdatedDate { get; set; } // datetime, null

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Update By")]
        public string UpdatedBy { get; set; } // nvarchar(50), null

        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public bool Status { get; set; } // bit, not null

        [Required(ErrorMessage = "Home Flag is required")]
        [Display(Name = "Home Flag")]
        public bool HomeFlag { get; set; } // bit, not null

        [MaxLength(500)]
        [StringLength(500)]
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

        [MaxLength(500)]
        [StringLength(500)]
        [Display(Name = "Description")]
        public string Description { get; set; } // nvarchar(500), null

        [MaxLength]
        [Display(Name = "Content")]
        public string Content { get; set; } // nvarchar(max), null

        [Required(ErrorMessage = "Price is required")]
        [Display(Name = "Price")]
        public decimal Price { get; set; } // decimal(18,2), not null

        [Display(Name = "Promotion Price")]
        public decimal? PromotionPrice { get; set; } // decimal(18,2), null

        [Display(Name = "Original Price")]
        public decimal OriginalPrice { get; set; }

        [Display(Name = "Warranty")]
        public int? Warranty { get; set; } // int, null

        [Display(Name = "Mfg Date")]
        public DateTime? ManufacturingDate { get; set; }

        [Display(Name = "Exp Date")]
        public DateTime? ExpireDate { get; set; }

        [Display(Name = "Quantity")]
        [Required]
        public int Quantity { get; set; }

        [ForeignKey("ID")]
        public virtual ProductCategory ProductCategory { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}