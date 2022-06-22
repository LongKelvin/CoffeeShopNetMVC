using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Web.Models
{
    public class FeedbackViewModel : ViewModelBase
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = nameof(Resources.Resources.RequiredNameMessage))]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = nameof(Resources.Resources.RequiredEmailMessage))]
        public string Email { get; set; }

        public string EmailSubject { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = nameof(Resources.Resources.RequiredFeedbackMessage))]
        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        public bool Status { get; set; }

        public string CaptchaCode { get; set; }
    }
}