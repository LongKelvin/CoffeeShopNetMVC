using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models.Models
{
    public class Status
    {
        [Key]
        [Required(ErrorMessage = "ID is required, It cannot be null or empty")]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; } // int, not null

        [Timestamp]
        public byte[] RowVersion { get; set; } //row version

        [Required]
        public int StatusCode { get; set; }

        [Required]
        [Display(Name = "Status Name")]
        public string StatusName { get; set; }

        [Display(Name = "Status Description")]
        public string StatusDescription { get; set; }

        [Required]
        public bool IsCanDelete { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}