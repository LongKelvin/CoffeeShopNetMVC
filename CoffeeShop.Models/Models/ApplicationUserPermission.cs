using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models.Models
{
    [Table("ApplicationUserPermissions")]
    public class ApplicationUserPermission
    {
        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string PermissionId { get; set; }

        public string RoleName { get; set; }

        [Key]
        [Column(Order = 3)]
        public virtual string RoleId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("PermissionId")]
        public virtual ApplicationPermission ApplicationPermission { get; set; }

        [ForeignKey("RoleId")]
        public virtual ApplicationRole ApplicationRoles { get; set; }
    }
}