using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Web.Models
{
    public class ApplicationPermissionViewModel
    {
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