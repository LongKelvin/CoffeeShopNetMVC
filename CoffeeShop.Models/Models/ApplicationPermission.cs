using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models.Models
{
    [Table("ApplicationPermissions")]
    public class ApplicationPermission
    {
        [Key]
        [Required(ErrorMessage = "Permission Id is required, It cannot be null or empty")]
        [Column(Order = 1)]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Module { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsSystemProtected { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; } //row version
    }
}