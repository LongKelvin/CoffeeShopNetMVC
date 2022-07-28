using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WkHtmlToPdfDotNet;

namespace CoffeeShop.PdfGenerator
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
