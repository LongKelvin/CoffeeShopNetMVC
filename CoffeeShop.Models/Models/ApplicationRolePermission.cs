using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models.Models
{
    [Table("ApplicationRolePermissions")]
    public class ApplicationRolePermission
    {
        [Key]
        [Required]
        [Column(Order = 1)]
        public string RoleId { get; set; }

        [Key]
        [Required, Column(Order = 2)]
        public string PermissionId { get; set; }

        [ForeignKey("PermissionId")]
        public virtual ApplicationPermission ApplicationPermission { get; set; }

        [ForeignKey("RoleId")]
        public virtual ApplicationRole ApplicationRole { get; set; }
    }
}