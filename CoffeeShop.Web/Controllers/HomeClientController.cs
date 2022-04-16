using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
    [RoutePrefix("Home")]
    public class HomeClientController : Controller
    {
        // GET: HomeClient
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult Footer()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult Category()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult Header()
        {
            return PartialView();
        }
    }
}