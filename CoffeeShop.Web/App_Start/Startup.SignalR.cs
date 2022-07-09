using Microsoft.AspNet.SignalR;

using Owin;

namespace CoffeeShop.Web.App_Start
{
    public partial class Startup
    {
        public void ConfigureSignalR(IAppBuilder app)
        {
            //Config signalR
            app.MapSignalR();
        }
    }
}