using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models.Models
{
    [Table("Menus")]
    public class Menu : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string URL { get; set; }

        public int? DisplayOrder { get; set; }

        [Required]
        public int GroupID { get; set; }

        public string Target { get; set; }

        [Required]
        public bool Status { get; set; }

        [ForeignKey("GroupID")]
        public virtual MenuGroup MenuGroup { get; set; }
    }
}