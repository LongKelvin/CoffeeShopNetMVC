using CoffeeShop.Models.Models;
using CoffeeShop.Services;
using CoffeeShop.Web.Resources;

using System;
using System.Web;
using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
    public class BaseController : Controller
    {
        private IErrorService _errorService;

        public BaseController(IErrorService errorService)
        {
            _errorService = errorService;
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string lang = null;
            HttpCookie langCookie = Request.Cookies["culture"];
            if (langCookie != null)
            {
                lang = langCookie.Value;
            }
            else
            {
                var userLanguage = Request.UserLanguages;
                var userLang = userLanguage != null ? userLanguage[0] : "";
                if (userLang != "")
                {
                    lang = userLang;
                }
                else
                {
                    lang = Resources.LanguageManagerment.GetDefaultLanguage();
                }
            }
            new LanguageManagerment().SetLanguage(lang);
            return base.BeginExecuteCore(callback, state);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Log any exceptions
            if (filterContext.Exception != null)
            {
                LogError(filterContext.Exception);
            }
        }

        private void LogError(Exception ex)
        {
            try
            {
                Error error = new Error
                {
                    CreatedDate = DateTime.Now,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                };

                _errorService.CreateError(error);
                _errorService.Save();
            }
            catch
            {
                throw;
            }
        }
    }
}