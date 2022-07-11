using CoffeeShop.Common;
using CoffeeShop.Models.Models;
using CoffeeShop.Services;
using CoffeeShop.Web.Infrastucture.Core;

using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CoffeeShop.Web.Api
{
    [RoutePrefix(Common.CommonConstants.API_Invoice)]
    public class InvoiceController : ApiControllerBase
    {
        private readonly IOrderInvoiceService _orderInvoiceService;
        private readonly IOrderService _orderService;

        public InvoiceController(IErrorService errorService, IOrderInvoiceService orderInvoiceService, IOrderService orderService) : base(errorService)
        {
            _orderInvoiceService = orderInvoiceService;
            _orderService = orderService;
        }

        [Route("CreateInvoice/{orderId:int}")]
        [HttpPost]
        public HttpResponseMessage CreateInvoice(HttpRequestMessage request, int orderId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var invoice = _orderInvoiceService.GetByCondition(x => x.Order.Equals(orderId));
                    if (invoice != null)
                    {
                        return response = request.CreateResponse(HttpStatusCode.OK);
                    }

                    var orderById = _orderService.GetById(orderId, new string[] { "OrderDetails" });

                    var newInvoice = new OrderInvoice();
                    newInvoice.InvoiceCode = Common.OrderInvoiceHelper.GenerateInvoiceCode(orderId.ToString());
                    newInvoice.CreatedDate = DateTime.Now;
                    newInvoice.Cashier = HttpContext.Current.User.Identity.Name;
                    newInvoice.OrderId = orderId;
                    newInvoice.Order = orderById;
                    newInvoice.Status = true;

                    _orderInvoiceService.CreateInvoice(newInvoice);
                    _orderInvoiceService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.Created, newInvoice);
                }

                return response;
            });
        }

        [Route("ExportInvoice/{orderId:int}")]
        [HttpGet]
        public async Task<HttpResponseMessage> ExportToPdf(HttpRequestMessage request, int orderId)
        {
            var invoiceById = _orderInvoiceService.GetByCondition(x => x.OrderId.Equals(orderId));
            var orderById = _orderService.GetById(orderId, new string[] { "OrderDetails" });

            string fileName = string.Concat("Invoice_" + invoiceById.OrderId + DateTime.Now.ToString("yyyyMMddhhmmsss") + ".pdf");
            var folderReport = ConfigHelper.GetByKey(CommonConstants.INVOICE_PDF_EXPORT_PATH);
            string filePath = HttpContext.Current.Server.MapPath(folderReport);

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            string fullPath = Path.Combine(filePath, fileName);

            invoiceById.Order = orderById;

            try
            {
                var data = invoiceById;
                //string htmlTemplate = File.ReadAllText(HttpContext.Current.Server.MapPath("/Assets/Admin/templates/product-report-template.html"));
                string razorViewTemplate = File.ReadAllText(HttpContext.Current.Server.MapPath("/Views/Shared/Templates/InvoiceTemplate.cshtml"));
                razorViewTemplate = razorViewTemplate.Replace("{{CreatedDate}}", DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
                var htmlParseViewData = RazorEngine.Razor.Parse(razorViewTemplate, data);

                await ReportHelper.GeneratePdf(htmlParseViewData, fullPath, CommonConstants.PDF_INVOICE_TYPE);
                Trace.WriteLine($"PATH DOWNLOAD URL: {Path.Combine(folderReport, fileName)}");
                return request.CreateResponse(HttpStatusCode.OK, Path.Combine(folderReport, fileName));
            }
            catch (Exception ex)
            {
                LogError(ex);
                return request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}