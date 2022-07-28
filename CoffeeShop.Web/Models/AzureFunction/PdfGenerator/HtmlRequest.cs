using WkHtmlToPdfDotNet;

namespace CoffeeShop.Web.Models.AzureFunction.PdfGenerator
{
    public class HtmlRequest
    {
        public string HtmlContent { get; set; }
        public string FilePath { get; set; }

        public string FileName { get; set; }

        public PaperKind PaperKind { get; set; }

        public Orientation Orientation { get; set; }
    }
}