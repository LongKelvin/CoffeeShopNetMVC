using CoffeeShop.Models.Abstract;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models.Models
{
    [Table("Orders")]
    public class Order : BaseEntity, ISwitchable
    {
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
            this.PaymentStatus = (int)Common.CommonConstants.PaymentStatus.Pending;
            this.OrderStatus = (int)Common.CommonConstants.OrderStatus.Pending;
            this.ShippingStatus = (int)Common.CommonConstants.ShippingStatus.NotYetShipped;
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

        [Required(ErrorMessage = "Payment Method Id is required")]
        [Display(Name = "Payment Method Id")]
        public int PaymentMethodID { get; set; } // nvarchar(250), not null

        [Range(0, 3, ErrorMessage = "Payment status value is not valid")]
        [Required(ErrorMessage = "Payment Status is required")]
        [Display(Name = "Payment Status")]
        public int PaymentStatus { get; set; } // nvarchar(50), not null

        [Range(0, 7, ErrorMessage = "Order status value is not valid")]
        [Display(Name = "Order Status")]
        [Required(ErrorMessage = "Order Status is required")]
        public int OrderStatus { get; set; }

        [Range(0, 4, ErrorMessage = "Shipping status value is not valid")]
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

        public string Note { get; set; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser User { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}