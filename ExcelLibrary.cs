using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace oms
{
    public static class ExcelLibrary
    {
        public static void UpdateDataTableToExcelMainData(DataTable table, string fileName)
        {
            using(Workbook workbook = new Workbook())
            {
                workbook.LoadDocument(fileName);
                
                foreach (Worksheet sheet in workbook.Sheets)
                {
                    if (sheet.Name.IsStringEqual("MainData"))
                    {
                        sheet.Cells.ClearContents();

                        List<String> columns = new List<string>();
                        int columnIndex = 0;
                        foreach (DataColumn column in table.Columns)
                        {
                            columns.Add(column.ColumnName);
                            sheet.Cells[0, columnIndex].Value = column.ColumnName;
                            columnIndex++;

                        }

                        int rowIndex = 1;
                        foreach (DataRow dsrow in table.Rows)
                        {
                            columnIndex = 0;
                            foreach (String col in columns)
                            {
                                if (typeof(Single) == dsrow[col].GetType())
                                {
                                    sheet.Rows[rowIndex][columnIndex].NumberFormat = "############.##";
                                    sheet.Rows[rowIndex][columnIndex].Value = CommonFunctions.GetFloatSafely(dsrow[col]);
                                }
                                else if (typeof(DateTime) == dsrow[col].GetType())
                                {
                                    sheet.Rows[rowIndex][columnIndex].NumberFormat = "d-mmm-yyyy";
                                    sheet.Rows[rowIndex][columnIndex].Value = CommonFunctions.GetDateTimeSafely(dsrow[col]);    
                                }
                                else
                                {
                                    sheet.Rows[rowIndex][columnIndex].Value = dsrow[col].ToString();
                                }

                                columnIndex++;
                            }

                            rowIndex++;
                        }
                    }
                }

                workbook.CalculateFullRebuild();

                workbook.SaveDocument(fileName);
            }
        }
    }
}
