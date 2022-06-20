using CoffeeShop.Services;
using CoffeeShop.Web.Infrastucture.Core;

using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoffeeShop.Web.Api
{
    [Authorize]
    [RoutePrefix(Common.CommonConstants.API_Statistic)]
    public class StatisticController : ApiControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticController(IErrorService errorService,
            IStatisticService statisticService) : base(errorService)
        {
            _statisticService = statisticService;
        }

        [Route("GetRevenue")]
        [HttpGet]
        [PermissionAuthorize(Common.ApplicationPermissons.Products.Edit)]
        [PermissionAuthorize(Common.ApplicationPermissons.Products.View)]
        [PermissionAuthorize(Common.ApplicationPermissons.Products.Delete)]
        public HttpResponseMessage GetRevenueStatistic(HttpRequestMessage request, string fromDate, string toDate)
        {
            if (string.IsNullOrEmpty(fromDate) || string.IsNullOrEmpty(toDate))
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid DateTime");

            return CreateHttpResponse(request, () =>
            {
                var result = _statisticService.GetRevenueStatistics(fromDate, toDate);

                if (result == null)
                {
                    return request.CreateErrorResponse(HttpStatusCode.NotFound, ModelState);
                }

                return request.CreateResponse(HttpStatusCode.OK, result);
            });
        }
    }
}