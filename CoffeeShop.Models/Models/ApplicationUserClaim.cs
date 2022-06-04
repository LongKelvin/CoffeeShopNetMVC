using Microsoft.AspNet.Identity.EntityFramework;

using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models.Models
{
    [Table("ApplicationUserClaims")]
    public class ApplicationUserClaim : IdentityUserClaim
    {
    }
}