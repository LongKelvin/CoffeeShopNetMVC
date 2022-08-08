using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

namespace CoffeeShop.PdfGenerator
{
    public class PdfGenerator
    {
        private readonly IConverter _converter;

        public PdfGenerator(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] GeneratePdfFileUsingWkHtmlToPdf(HtmlRequest htmlRequest)
        {
            //Func<object, byte[]> funcGeneratePdfFile = (request) =>
            //{
            //};

            //var pdfGen = new Task<byte[]>(funcGeneratePdfFile, new { Orientation = htmlRequest.Orientation, PaperKind = htmlRequest.PaperKind, HtmlContent = htmlRequest.HtmlContent });
            //pdfGen.Start();

            try
            {
                //dynamic requestData = request;
                //var converter = new SynchronizedConverter(new PdfTools());
                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings = {
                            ColorMode = ColorMode.Color,
                            Orientation = htmlRequest.Orientation,
                            PaperSize = htmlRequest.PaperKind,
                            Margins = new MarginSettings() { Top = 10 },
                            DPI = 320
                         },
                    Objects = {
                           new ObjectSettings() {
                                PagesCount = true,
                                HtmlContent =   htmlRequest.HtmlContent,
                                WebSettings = { DefaultEncoding = "utf-8" },
                                //HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                            },
                        }
                };

                return _converter.Convert(doc);
            }
            catch
            {
                return null;
                throw;
            }
        }

        [FunctionName("GeneratePdf")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HtmlRequest req,
            ILogger log)
        {
            //log.LogInformation("C# HTTP trigger function processed a request. PDF generator");

            var pdfBytes = GeneratePdfFileUsingWkHtmlToPdf(req);
            var response = BuildResponse(pdfBytes);

            return response;
        }

        private static FileContentResult BuildResponse(byte[] pdfBytes)
        {
            return new FileContentResult(pdfBytes, "application/pdf");
        }
    }
}