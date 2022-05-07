using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Models.Models
{
    [Table("Feedbacks")]
    public class Feedback: BaseEntity
    {
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredNameMessage))]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredEmailMessage))]
        public string Email { get; set; }

        public string EmailSubject { get; set; }

        [Required(ErrorMessageResourceName = nameof(Resource.RequiredNameMessage))]
        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}
