using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Models.Models
{
    public class BaseEntity
    {
        [Key]
        [Required(ErrorMessage = "ID is required")]
        [Display(Name = "ID")]
        public int ID { get; set; } // int, not null

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
