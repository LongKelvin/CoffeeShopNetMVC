using CoffeeShop.Models.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Web.Models
{
    public class OrderDetailViewModel
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

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }

        public string ProductName { get; set; }
        public string ProductImage { get; set; }

        public decimal TotalPrice { get; set; }

    }
}