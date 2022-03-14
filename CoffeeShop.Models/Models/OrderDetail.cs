using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models.Models
{
    [Table("OrderDetails")]
    public class OrderDetail : BaseEntity
    {
        [Key]
        [Column(Order = 1)]
        [Required(ErrorMessage = "Order ID is required")]
        [Display(Name = "Order ID")]
        public int OrderID { get; set; } // int, not null

        [Key]
        [Column(Order = 2)]
        [Required(ErrorMessage = "Product ID is required")]
        [Display(Name = "Product ID")]
        public int ProductID { get; set; } // int, not null

        [Display(Name = "Quantity")]
        public int? Quantity { get; set; } // int, null

        [ForeignKey("ID")]
        public virtual Order Order { get; set; }

        [ForeignKey("ID")]
        public virtual Product Product { get; set; }
    }
}
