using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models.Models
{
    [Table("Feedbacks")]
    public class Feedback : BaseEntity
    {
        [Required(ErrorMessageResourceType = typeof(Resource),
            ErrorMessageResourceName = nameof(Resource.RequiredNameMessage))]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessageResourceType = typeof(Resource),
            ErrorMessageResourceName = nameof(Resource.RequiredEmailMessage))]
        public string Email { get; set; }

        public string EmailSubject { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource),
            ErrorMessageResourceName = nameof(Resource.RequiredNameMessage))]
        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}