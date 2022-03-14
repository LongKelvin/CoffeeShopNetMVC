using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models.Models
{
    [Table("VisitorStatistics")]
    public class VisitorStatistic
    {
        [Key]
        [Required(ErrorMessage = "ID is required")]
        [Display(Name = "ID")]
        public Guid ID { get; set; } // uniqueidentifier, not null

        [Required(ErrorMessage = "Visited Date is required")]
        [Display(Name = "Visited Date")]
        public DateTime VisitedDate { get; set; } // datetime, not null

        [MaxLength(50)]
        [StringLength(50)]
        [Required(ErrorMessage = "IP Address is required")]
        [Display(Name = "IP Address")]
        public string IPAddress { get; set; } // varchar(50), not null
    }
}