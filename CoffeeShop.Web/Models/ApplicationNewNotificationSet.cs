using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeShop.Web.Models
{
    public class ApplicationNotificationSet
    {
        public List<ApplicationNotificationViewModel> ListNotification { get; set; }

        public int TotalNewNotification { get; set; }
    }
}