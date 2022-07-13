using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models.Models
{
    [Table("ApplicationNotifications")]
    public class ApplicationNotification : BaseEntity
    {
        public string Message { get; set; }
        public string Url { get; set; }

        public bool Status { get; set; }

        public bool IsReaded { get; set; }

        public string Type { get; set; }
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// ExtraValue use to store extra information about notification
        /// Most of them store id of object type 
        /// For this, we can access the detail about notifications
        /// </summary>
        public string ExtraValue { get; set; }
    }
}