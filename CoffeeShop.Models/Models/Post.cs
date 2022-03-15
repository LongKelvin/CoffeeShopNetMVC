using CoffeeShop.Models.Abstract;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models.Models
{
    [Table("Posts")]
    public class Post : BaseEntity, IAuditable, ISeoable, ISwitchable
    {
        public Post()
        {
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
        public DateTime? CreateDate { get; set; } // datetime, null

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Create By")]
        public string CreateBy { get; set; } // nvarchar(50), null

        [Display(Name = "Update Date")]
        public DateTime? UpdateDate { get; set; } // datetime, null

        [MaxLength(50)]
        [StringLength(50)]
        [Required(ErrorMessage = "Update By is required")]
        [Display(Name = "Update By")]
        public string UpdateBy { get; set; } // nvarchar(50), not null

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

        [ForeignKey("ID")]
        public virtual PostCategory PostCategory { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}