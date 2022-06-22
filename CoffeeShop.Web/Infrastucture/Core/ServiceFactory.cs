using System.Web;
using System.Web.Mvc;

namespace CoffeeShop.Web.Infrastucture.Core
{
    public static class ServiceFactory
    {
        /// <summary>
        /// THelper Get<THelper> allow us to get instance of services without controller inject
        /// </summary>
        /// <typeparam name="THelper"></typeparam>
        /// <returns></returns>
        public static THelper Get<THelper>()
        {
            if (HttpContext.Current != null)
                return DependencyResolver.Current.GetService<THelper>();

            var key = string.Concat("factory", typeof(THelper).Name);
            if (!HttpContext.Current.Items.Contains(key))
            {
                var resolvedService = DependencyResolver.Current.GetService<THelper>();
                HttpContext.Current.Items.Add(key, resolvedService);
            }
            return (THelper)HttpContext.Current.Items[key];
        }
    }
}