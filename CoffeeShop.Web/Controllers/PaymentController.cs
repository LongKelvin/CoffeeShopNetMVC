using CoffeeShop.Services;

using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
    public class PaymentController : BaseController
    {
        public PaymentController(IErrorService errorService) : base(errorService)
        {
        }

        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }
    }
}