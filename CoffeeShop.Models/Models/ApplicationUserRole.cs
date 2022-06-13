using Microsoft.AspNet.Identity.EntityFramework;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Models.Models
{
    [Table("ApplicationUserRoles")]
    public class ApplicationUserRole: IdentityUserRole
    {
    }
}
