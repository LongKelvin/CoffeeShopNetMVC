using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models.Models
{
    [Table("SystemConfigs")]
    public class SystemConfig : BaseEntity
    {
       

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Code")]
        public string Code { get; set; } // nvarchar(50), null

        [MaxLength(250)]
        [StringLength(250)]
        [Display(Name = "Value String")]
        public string ValueString { get; set; } // nvarchar(250), null

        [Display(Name = "Value Int")]
        public int? ValueInt { get; set; } // int, null
    }
}
