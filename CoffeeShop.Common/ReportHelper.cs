using OfficeOpenXml;

using PdfSharp;

using SelectPdf;

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

        public static async Task GeneratePdfInvoice(string htmlTemplateContent, string filePath)
        {
            await Task.Run(() =>
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    // instantiate a html to pdf converter object
                    HtmlToPdf converter = new HtmlToPdf
                    {
                        Options =
                        {
                            PdfPageSize = PdfPageSize.A4,
                            PdfPageOrientation = PdfPageOrientation.Portrait,
                        }
                    };

                    PdfDocument doc = converter.ConvertHtmlString(htmlTemplateContent, filePath);
                    // save pdf document
                    doc.Save(fs);
                    // close pdf document
                    doc.Close();
                }
            });
        }

        public static async Task GeneratePdf(string htmlTemplate, string filePath, string type)
        {
            await Task.Run(() =>
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    if (type == CommonConstants.PDF_INVOICE_TYPE)
                    {
                        var pdf = PdfGenerator.GeneratePdf(htmlTemplate, InvoicePdfConfig());
                        pdf.Save(fs);
                    }
                    else
                    {
                        var pdf = PdfGenerator.GeneratePdf(htmlTemplate, DefaultPdfConfig());
                        pdf.Save(fs);
                    }
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

        public static PdfGenerateConfig InvoicePdfConfig()
        {
            return new PdfGenerateConfig
            {
                PageSize = PageSize.A5,
                MarginTop = 5,
                MarginBottom = 5,
                MarginLeft = 5,
                MarginRight = 5,
                PageOrientation = PageOrientation.Portrait,
            };
        }
    }
}