using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Com.Ericmas001.Util;
using Excel = Microsoft.Office.Interop.Excel;

namespace Com.Ericmas001.Data
{
    public class ExcelExporter
    {
        public event IntHandler ProgressUpdated = delegate { };
        public event IntHandler ExportationStarted = delegate { };
        public event BooleanHandler ExportationEnded = delegate { };

        public void ExportDataTable(DataTable table)
        {
            ExportationStarted(table.Rows.Count);
            new Thread(new ThreadStart(delegate
            {
                try
                {
                    Excel.Application excelApp = new Excel.Application();
                    Excel.Workbook excelWorkbook = excelApp.Workbooks.Add();
                    Excel.Worksheet sheet = excelWorkbook.Worksheets.Add();
                    foreach (Excel.Worksheet sh in excelWorkbook.Worksheets)
                        if (sh != sheet)
                            sh.Delete();
                    sheet.Name = String.IsNullOrWhiteSpace(table.TableName) ? "Table" : table.TableName;
                    string[] cols = table.Columns.OfType<DataColumn>().Select(dc => dc.ColumnName).ToArray();

                    Excel.Range excelHeaders = (Excel.Range)sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, cols.Length]];
                    excelHeaders.Value2 = cols;
                    excelHeaders.Font.Bold = true;
                    excelHeaders.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    excelHeaders.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    int i = 2;
                    foreach (DataRow dr in table.Rows)
                    {
                        ProgressUpdated(i - 1);
                        string[] data = new string[cols.Length];
                        for (int j = 0; j < data.Length; ++j)
                            data[j] = dr[j].ToString();
                        Excel.Range excelRow = (Excel.Range)sheet.Range[sheet.Cells[i, 1], sheet.Cells[i, cols.Length]];
                        excelRow.Value2 = data;
                        i++;
                    }
                    sheet.Application.ActiveWindow.SplitRow = 1;
                    sheet.Application.ActiveWindow.FreezePanes = true;
                    Excel.Range firstRow = (Excel.Range)sheet.Rows[1];
                    firstRow.AutoFilter(1, Type.Missing, Excel.XlAutoFilterOperator.xlAnd, Type.Missing, true);
                    firstRow.EntireColumn.AutoFit();
                    excelApp.Visible = true;
                    ExportationEnded(true);
                }
                catch
                {
                    ExportationEnded(false);
                }
            }
                    )).Start();
        }
    }
}
