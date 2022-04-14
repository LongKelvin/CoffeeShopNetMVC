using CoffeeShop.Services;
using CoffeeShop.Web.Infrastucture.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CoffeeShop.Web.Api
{
    [RoutePrefix("api/Home")]
    [Authorize]
    public class HomeController : ApiControllerBase
    {
        IErrorService _errorService;
        public HomeController(IErrorService errorService) : base(errorService)
        {
            _errorService = errorService;
        }

        [HttpGet]
        [Route("TestMethod")]
        public string TestMothod()
        {
            return "Login success";
        }
    }
}