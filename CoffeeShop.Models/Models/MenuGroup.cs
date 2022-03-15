using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models.Models
{
    [Table("MenuGroups")]
    public class MenuGroup : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public virtual IEnumerable<Menu> Menu { get; set; }
    }
}