using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Models.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Data.Repositories
{
    public interface IApplicationNotificationRepository : IRepository<ApplicationNotification>
    {
        List<ApplicationNotification> GetTop10NewNotification();
    }
    public class ApplicationNotificationRepository : RepositoryBase<ApplicationNotification>, IApplicationNotificationRepository
    {
        public ApplicationNotificationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public List<ApplicationNotification> GetTop10NewNotification()
        {
            return DbContext.ApplicationNotifications.Where(x => x.Status == true).Take(10).ToList();
        }
    }
}
