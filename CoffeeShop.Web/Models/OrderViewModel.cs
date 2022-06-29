using CoffeeShop.Models.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Web.Models
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
        }

        [MaxLength(250)]
        [StringLength(250)]
        [Required(ErrorMessage = "Customer Name is required")]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; } // nvarchar(250), not null

        [MaxLength(250)]
        [StringLength(250)]
        [Required(ErrorMessage = "Customer Address is required")]
        [Display(Name = "Customer Address")]
        public string CustomerAddress { get; set; } // nvarchar(250), not null

        [MaxLength(250)]
        [StringLength(250)]
        [Required(ErrorMessage = "Customer Email is required")]
        [Display(Name = "Customer Email")]
        public string CustomerEmail { get; set; } // nvarchar(250), not null

        [MaxLength(50)]
        [StringLength(50)]
        [Required(ErrorMessage = "Customer Mobile is required")]
        [Display(Name = "Customer Mobile")]
        public string CustomerMobile { get; set; } // nvarchar(50), not null

        [MaxLength(500)]
        [StringLength(500)]
        [Display(Name = "Customer Message")]
        public string CustomerMessage { get; set; } // nvarchar(500), null

        [Display(Name = "Create Date")]
        public DateTime? CreatedDate { get; set; } // datetime, null

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Create By")]
        public string CreatedBy { get; set; } // nvarchar(50), null

        [Required(ErrorMessage = "Payment Method is required")]
        [Display(Name = "Payment Method")]
        public int PaymentMethodCode { get; set; } // nvarchar(250), not null

        [Range(0, 2, ErrorMessage = "Payment status value is not valid")]
        [Required(ErrorMessage = "Payment Status is required")]
        [Display(Name = "Payment Status")]
        public int PaymentStatus { get; set; } // nvarchar(50), not null

        [Range(0, 4, ErrorMessage = "Order status value is not valid")]
        [Display(Name = "Order Status")]
        [Required(ErrorMessage = "Order Status is required")]
        public int OrderStatus { get; set; }

        [Range(0, 3, ErrorMessage = "Shipping status value is not valid")]
        [Display(Name = "Shipping Status")]
        [Required(ErrorMessage = "Shipping Status is required")]
        public int ShippingStatus { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public bool Status { get; set; }

        [StringLength(128)]
        [Column(TypeName = "nvarchar")]
        public string CustomerId { get; set; }

        public decimal? TotalAmount { get; set; }

        public decimal? TotalItemPrice { get; set; }

        public decimal? ShippingFee { get; set; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser User { get; set; }

        //public virtual PaymentMethodViewModel PaymentMethod { get; set; }
    }
}