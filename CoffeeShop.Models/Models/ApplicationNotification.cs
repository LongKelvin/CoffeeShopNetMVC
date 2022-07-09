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
    }
}