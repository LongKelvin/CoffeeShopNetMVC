using CoffeeShop.Services;

using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
    public class CartController : BaseController
    {
        public CartController(IErrorService errorService) : base(errorService)
        {
        }

        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
    }
}