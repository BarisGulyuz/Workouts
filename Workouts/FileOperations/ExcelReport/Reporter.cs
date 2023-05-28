using ClosedXML.Excel;
namespace Workouts.ExcelReport
{
    public class Reporter
    {
        public static bool ExportToExcel<T>(List<T> datas, string sheetName = "", string path = "")
        {
            if (string.IsNullOrEmpty(path)) path = "D:\\Codes\\";
            if (string.IsNullOrEmpty(sheetName)) sheetName = typeof(T).Name;
            string file = $"{path}{sheetName}.xlsx";

            List<string> columns = typeof(T).GetProperties().Select(x => x.Name).ToList();

            using XLWorkbook workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(sheetName);

            int row = 1;
            int column = 1;

            foreach (string columnName in columns)
            {
                IXLCell cell = worksheet.Cell(row, column);
                cell.Value = columnName;
                cell.Style.Fill.BackgroundColor = XLColor.BallBlue;
                column++;
            }
            row++;
            column = 1;

            foreach (T data in datas)
            {
                foreach (string columnName in columns)
                {
                    worksheet.Cell(row, column).Value = data?.GetType().GetProperty(columnName)?.GetValue(data);
                    column++;
                }
                row++;
                column = 1;
            }

            workbook.SaveAs(file);
            return true;
        }
    }
}
