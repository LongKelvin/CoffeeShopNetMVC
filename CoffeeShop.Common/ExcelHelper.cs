using OfficeOpenXml;

using System.Collections.Generic;
using System.Linq;

namespace CoffeeShop.Common
{
    public static class ExcelHelper
    {
        /// <summary>
        /// Return True or False value from Excel worksheet cell
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ExtractTrueFalseValue(object value)
        {
            if (value == null)
                return false;

            if (value is bool)
                return (bool)value;

            string valueString = value.ToString().ToLower();
            if (!string.IsNullOrEmpty(valueString))
                if (valueString == "true")
                    return true;

            return false;

            //switch (value.ToString())
            //{
            //    case "True":
            //    case "TRUE":
            //    case "true":
            //    case "1":
            //        return true;

            //    default: return false;
            //}
        }

        public static void TrimLastEmptyRows(this ExcelWorksheet worksheet)
        {
            while (worksheet.IsLastRowEmpty())
                worksheet.DeleteRow(worksheet.Dimension.End.Row);
        }

        public static bool IsLastRowEmpty(this ExcelWorksheet worksheet)
        {
            var empties = new List<bool>();

            for (int i = 1; i <= worksheet.Dimension.End.Column; i++)
            {
                var rowEmpty = worksheet.Cells[worksheet.Dimension.End.Row, i].Value == null ? true : false;
                empties.Add(rowEmpty);
            }

            return empties.All(e => e);
        }
    }
}