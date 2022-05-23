using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models.Models
{
    [Table("ApplicationRoleGroups")]
    public class ApplicationRoleGroup
    {
        [Key]
        [Column(Order = 1)]
        public int GroupId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(128)]
        public string RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual ApplicationRole ApplicationRole { get; set; }

        [ForeignKey("GroupId")]
        public virtual ApplicationGroup ApplicationGroup { get; set; }
    }
}