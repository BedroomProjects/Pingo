using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;       //microsoft Excel 14 object in references-> COM tab

namespace WatanyaPingoConsole
{
    public class ExcelToNode
    {

        public static List<List<string>> result = new List<List<string>>();

        public static void getExcelFile(string fileName)
        {
            //string curFileDirectory = Directory.GetCurrentDirectory() + fileName;
            string curFileDirectory = @"C:\Network Schemes" + fileName;  
            try
            {
                //Create COM Objects. Create a COM object for everything that is referenced
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(curFileDirectory);
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Excel.Range xlRange = xlWorksheet.UsedRange;

                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;


                for (int i = 1; i <= rowCount; i++)
                {
                    List<string> cur = new List<string>();
                    cur.Add(xlRange.Cells[i, 1].Value2.ToString());
                    cur.Add(xlRange.Cells[i, 2].Value2.ToString());
                    result.Add(cur);
                }

                //cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();

                //rule of thumb for releasing com objects:
                //  never use two dots, all COM objects must be referenced and released individually
                //  ex: [somthing].[something].[something] is bad

                //release com objects to fully kill excel process from running in the background
                Marshal.ReleaseComObject(xlRange);
                Marshal.ReleaseComObject(xlWorksheet);

                //close and release
                xlWorkbook.Close();
                Marshal.ReleaseComObject(xlWorkbook);

                //quit and release
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);
            }
            catch (Exception e)
            {
                Console.SetCursorPosition(0, 4);
                Console.WriteLine("Error: Can not read excel file " + curFileDirectory + "\nCheck if file exists.");
            }
        }

        public static List<List<string>> getResult()
        {
            return result;
        }
    }
}