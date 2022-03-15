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
        public DateTime? CreateDate { get; set; } // datetime, null

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Create By")]
        public string CreateBy { get; set; } // nvarchar(50), null

        [MaxLength(250)]
        [StringLength(250)]
        [Required(ErrorMessage = "Payment Mehod is required")]
        [Display(Name = "Payment Mehod")]
        public string PaymentMehod { get; set; } // nvarchar(250), not null

        [MaxLength(50)]
        [StringLength(50)]
        [Required(ErrorMessage = "Payment Status is required")]
        [Display(Name = "Payment Status")]
        public string PaymentStatus { get; set; } // nvarchar(50), not null

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public bool Status { get; set; }
    }
}