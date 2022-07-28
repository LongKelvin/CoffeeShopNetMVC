using CoffeeShop.Common;
using CoffeeShop.Models.Models;
using CoffeeShop.Services;
using CoffeeShop.Web.Models.AzureFunction.PdfGenerator;

using Newtonsoft.Json;

using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

using WkHtmlToPdfDotNet;

namespace CoffeeShop.Web.Infrastucture.Core
{
    public class ApiControllerBase : ApiController, IExceptionLogger
    {
        private IErrorService _errorService;

        public bool AllowMultiple => throw new NotImplementedException();

        public ApiControllerBase(IErrorService errorService)
        {
            _errorService = errorService;
        }

        protected HttpResponseMessage CreateHttpResponse(
            HttpRequestMessage requestMessage, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = function.Invoke();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
                LogError(ex);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (DbUpdateException dbex)
            {
                LogError(dbex);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbex.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        public void LogError(Exception ex)
        {
            try
            {
                if (ex == null)
                {
                    var errorDetail = $"Error orccur in {nameof(LogError)} - controller {nameof(ApiControllerBase)} - Exception was null but error invoke.";
                    Error e = new Error
                    {
                        CreatedDate = DateTime.Now,
                        Message = errorDetail,
                    };
                    _errorService.CreateError(e);
                }
                else
                {
                    Error error = new Error
                    {
                        CreatedDate = DateTime.Now,
                        Message = ex.Message,
                        StackTrace = ex.StackTrace
                    };

                    _errorService.CreateError(error);
                }
            }
            catch
            {
                throw;
            }
        }

        public Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            LogError(context.Exception);
            return Task.FromResult(0);
        }

        public async Task<byte[]> GeneratePdfFromAzureFunction(string htmlContent, string fileName, PaperKind paperKind = PaperKind.A4, Orientation orientation = Orientation.Portrait)
        {
            var azurePdfFunctionUrl = ConfigHelper.GetByKey(CommonConstants.AZURE_PDF_FUNCTION_URL);
            using (HttpClient client = new HttpClient())
            {
                var req = new HtmlRequest
                {
                    HtmlContent = htmlContent,
                    FileName = fileName,
                    PaperKind = paperKind,
                    Orientation = orientation
                };
                string json = JsonConvert.SerializeObject(req);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await client.PostAsync(azurePdfFunctionUrl, byteContent);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsByteArrayAsync();
            }
        }
    }
}