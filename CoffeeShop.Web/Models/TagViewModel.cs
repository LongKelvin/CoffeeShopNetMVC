using CoffeeShop.Models.Models;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Web.Models
{
    public class TagViewModel
    {
        
       
        [Display(Name = "ID")]
        public string ID { get; set; } // varchar(50), not null

       
        [Display(Name = "Name")]
        public string Name { get; set; } // nvarchar(50), null

     
        [Display(Name = "Type")]
        public string Type { get; set; } // varchar(50), null

        public virtual ICollection<PostViewModel> Posts { get; set; }

        public virtual ICollection<ProductViewModel> Products { get; set; }
    }
}