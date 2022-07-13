using Microsoft.AspNet.SignalR;

namespace CoffeeShop.Web
{

    public class NotificationHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.addNewMessageToPage(message);
        }

        public static void SendNotification(string message)
        {
            // Call the addNewMessageToPage method to update clients.
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.addNewMessageToPage(message);
        }
    }
}