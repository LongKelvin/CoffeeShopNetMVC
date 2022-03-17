using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeShop.Web.Models
{
    public class ViewModelBase
    {
        public int ID { get; set; }
        public byte[] RowVersion { get; set; }
    }
}