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
            IAsyncResult result = null;
            try
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
                result = base.BeginExecuteCore(callback, state);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }

            return result;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Log any exceptions
            if (filterContext.Exception != null)
            {
                LogError(filterContext.Exception);
            }
        }

        public void LogError(Exception ex)
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
            }
            catch
            {
                throw;
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;

            //Log the error!!
            LogError(filterContext.Exception);

            //Redirect or return a view, but not both.
            //filterContext.Result = RedirectToAction("Index", "ErrorHandler");
            // OR
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/ErrorHandler/Index.cshtml"
            };
        }
    }
}