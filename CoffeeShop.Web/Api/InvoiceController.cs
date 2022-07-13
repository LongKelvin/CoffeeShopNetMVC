using CoffeeShop.Common;
using CoffeeShop.Models.Models;
using CoffeeShop.Services;
using CoffeeShop.Web.Infrastucture.Core;

using System;
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
                    return request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var invoice = _orderInvoiceService.GetByCondition(x => x.OrderId.Equals(orderId));
                    if (invoice != null)
                    {
                        return request.CreateResponse(HttpStatusCode.OK);
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
            var invoiceById = _orderInvoiceService.GetByCondition(x => x.OrderId == (orderId));
            var orderById = _orderService.GetById(orderId, new string[] { "OrderDetails" });
            invoiceById.Order = orderById;

            var htmlTemplatePath = HttpContext.Current.Server.MapPath("/Views/Shared/Templates/InvoiceTemplate.cshtml");
            var renderHtmlTask = RenderRazorViewAsync(invoiceById, htmlTemplatePath);

            var folderReport = ConfigHelper.GetByKey(CommonConstants.INVOICE_PDF_EXPORT_PATH);
            var filePath = HttpContext.Current.Server.MapPath(folderReport);
            var createPdfPathTask = CreateInvoicePathAsync(invoiceById.OrderId.ToString(), filePath);

            await Task.WhenAll(renderHtmlTask, createPdfPathTask);

            try
            {
                await ReportHelper.GeneratePdf(renderHtmlTask.Result, createPdfPathTask.Result[0], CommonConstants.PDF_INVOICE_TYPE);
                return request.CreateResponse(HttpStatusCode.OK, Path.Combine(folderReport, createPdfPathTask.Result[1]));
            }
            catch (Exception ex)
            {
                LogError(ex);
                return request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// This function is for rendering the Razor view to HTML.
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        private async Task<string> RenderRazorViewAsync(OrderInvoice invoice, string templatePath)
        {
            Func<object, string> funcRenderHtmlView = (x) =>
            {
                string razorViewTemplate = File.ReadAllText(templatePath);
                //razorViewTemplate = razorViewTemplate.Replace("{{CreatedDate}}", DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
                return RazorEngine.Razor.Parse(razorViewTemplate, x);
            };

            var task = new Task<string>(funcRenderHtmlView, invoice);
            task.Start();

            return await task;
        }

        /// <summary>
        /// This function aim to create invoice path
        /// Result from this function return an string[] with 2 element:
        /// fullPath: the full path of file that pdf will be created
        /// fileName: the name of file
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        private async Task<string[]> CreateInvoicePathAsync(string orderId, string filePath)
        {
            Func<object, string[]> funcCreateInvoicePath = (x) =>
            {
                dynamic f = x;
                string fileName = string.Concat("Invoice_" + f.orderIdStr + DateTime.Now.ToString("yyyyMMddhhmmsss") + ".pdf");

                if (!Directory.Exists(f.filePath))
                    Directory.CreateDirectory(f.filePath);

                var fullPath = Path.Combine(filePath, fileName);
                return new string[] { fullPath, fileName };
            };

            var task = new Task<string[]>(funcCreateInvoicePath, new { orderIdStr = orderId, filePath = filePath });
            task.Start();
            return await task;
        }
    }
}