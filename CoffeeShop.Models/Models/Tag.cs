using Newtonsoft.Json;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models.Models
{
    [Table("Tags")]
    public class Tag
    {
        public Tag()
        {
            this.Posts = new HashSet<Post>();
            this.Products = new HashSet<Product>();
        }

        [Key]
        [MaxLength(50)]
        [StringLength(50)]
        [Required(ErrorMessage = "ID is required")]
        [Display(Name = "ID")]
        public string ID { get; set; } // varchar(50), not null

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; } // nvarchar(50), null

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Type")]
        public string Type { get; set; } // varchar(50), null

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}