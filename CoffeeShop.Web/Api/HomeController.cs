using CoffeeShop.Services;
using CoffeeShop.Web.Infrastucture.Core;

using System.Net;
using System.Web.Http;

namespace CoffeeShop.Web.Api
{
    [RoutePrefix(Common.CommonConstants.API_Home)]
    [Authorize]
    public class HomeController : ApiControllerBase
    {
        private IErrorService _errorService;

        public HomeController(IErrorService errorService) : base(errorService)
        {
            _errorService = errorService;
        }

        [HttpGet]
        [Route("TestMethod")]
        public string TestMothod()
        {
            return (HttpStatusCode.Unauthorized).ToString();
        }
    }
}