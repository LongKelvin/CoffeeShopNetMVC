using OfficeOpenXml;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

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
    }
}
