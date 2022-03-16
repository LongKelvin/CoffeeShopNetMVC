using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models.Models
{
    public class BaseEntity
    {
        [Key]
        [Required(ErrorMessage = "ID is required, It cannot be null or empty")]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; } // int, not null

        [Timestamp]
        public byte[] RowVersion { get; set; } //row version
    }
}