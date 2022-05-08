using CoffeeShop.Services;

using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
    public class AdminController : BaseController
    {
        public AdminController(IErrorService errorService) : base(errorService)
        {
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}