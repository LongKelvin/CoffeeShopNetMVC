using OfficeOpenXml;

using PdfSharp;

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace CoffeeShop.Common
{
    public static class ReportHelper
    {
        public static Task GenerateExcelXls<T>(List<T> datasource, string filePath)
        {
            return Task.Run(() =>
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(nameof(T));
                    worksheet.Cells["A1"].LoadFromCollection<T>(datasource, true, OfficeOpenXml.Table.TableStyles.Light20);
                    worksheet.Cells.AutoFitColumns();
                    package.Save();
                }
            });
        }

        public static async Task GeneratePdf(string htmlTemplate, string filePath)
        {
            await Task.Run(() =>
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    var pdf = PdfGenerator.GeneratePdf(htmlTemplate, DefaultPdfConfig());
                    pdf.Save(fs);
                }
            });
        }

        public static PdfGenerateConfig DefaultPdfConfig()
        {
            return new PdfGenerateConfig
            {
                PageSize = PageSize.A4,
                MarginTop = 5,
                MarginBottom = 5,
                MarginLeft = 25,
                MarginRight = 5,
                PageOrientation = PageOrientation.Landscape,
            };
        }
    }
}