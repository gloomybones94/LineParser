using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace LineParser
{
    static class FileSplitter
    {
        public static bool SplitFile(string reportPath, string destinationDir)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Create directory
            Directory.CreateDirectory(destinationDir);

            try
            {
                // Get raw report file
                ExcelPackage excelFile = new(new FileInfo(reportPath));

                // Break into individual sheets and save
                foreach (var sheet in excelFile.Workbook.Worksheets)
                {
                    string newFileName;

                    newFileName = sheet.Name.Replace(" ", "-").ToLower() + ".xlsx";

                    ExcelPackage newFile = new(new FileInfo(destinationDir + newFileName));
                    newFile.Workbook.Worksheets.Add(newFileName);
                    var newSheet = newFile.Workbook.Worksheets[0];

                    int lastCell = sheet.Dimension.End.Row - 1;
                    var sourceRange = sheet.Cells["A1:I" + lastCell.ToString()];
                    sourceRange.Copy(newSheet.Cells["A1"]);

                    var format = new ExcelOutputTextFormat
                    {
                        TextQualifier = '"',
                        Encoding = Encoding.UTF8,
                        EOL = "\r\n"
                    };

                    newSheet.Column(3).Style.Numberformat.Format = "@";
                    newSheet.DeleteColumn(10, 16374);

                    var file = new FileInfo(destinationDir + sheet.Name.Replace(" ", "-").ToLower() + ".csv");

                    newSheet.Cells["A1:I" + lastCell.ToString()].SaveToText(file, format);
                }
                return true;

            }
            catch
            {
                return false;
            }
        }
    }
}
