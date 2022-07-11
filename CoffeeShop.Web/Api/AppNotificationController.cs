using AutoMapper;

using CoffeeShop.Models.Models;
using CoffeeShop.Services;
using CoffeeShop.Web.Infrastucture.Core;
using CoffeeShop.Web.Models;

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoffeeShop.Web.Api
{
    [RoutePrefix(Common.CommonConstants.API_AppNotification)]
    public class AppNotificationController : ApiControllerBase
    {
        private readonly IApplicationNotificationService _appNotificationService;

        public AppNotificationController(
            IApplicationNotificationService appNotificationService,
            IErrorService errorService) : base(errorService)
        {
            _appNotificationService = appNotificationService;
        }

        [HttpGet]
        [Route("GetTop10Notification")]
        public HttpResponseMessage GetTop10Notification(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listResult = _appNotificationService.GetTop10NewNotification().OrderBy(x => x.ID);

                var listNotification = new ApplicationNotificationSet
                {
                    ListNotification = Mapper.Map<IEnumerable<ApplicationNotification>, IEnumerable<ApplicationNotificationViewModel>>(listResult).ToList(),
                    TotalNewNotification = listResult.Where(x => x.IsReaded.Equals(false)).Count()
                };

                return request.CreateResponse(HttpStatusCode.OK, listNotification);
            });
        }

        [HttpPost]
        [Route("UpdateReadedStatus/{id:int}")]
        public HttpResponseMessage UpdateNotificationStatusAsReaded(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var updateNotification = _appNotificationService.GetById(id);
                if (updateNotification == null)
                    return request.CreateResponse(HttpStatusCode.NotFound, "Notification not found");

                if (updateNotification.IsReaded)
                    return request.CreateResponse(HttpStatusCode.OK, updateNotification);

                _appNotificationService.MakeAsReadedNotification(updateNotification.ID);
                _appNotificationService.SaveChanges();

                return request.CreateResponse(HttpStatusCode.OK, updateNotification);
            });
        }
    }
}