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

            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Console.WriteLine(Directory.GetCurrentDirectory() + fileName);
            Console.ReadKey();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(Directory.GetCurrentDirectory() + fileName);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!
            //for (int i = 1; i <= rowCount; i++)
            //{
            //    for (int j = 1; j <= colCount; j++)
            //    {
            //        //new line
            //        if (j == 1)
            //            Console.Write("\r\n");

            //        //write the value to the console
            //        if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
            //            Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t" + "(" + i + "," + j + ")");
            //    }
            //}

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

        public static List<List<string>> getResult()
        {
            return result;
        }

        //List<NetworkNode> nn = new List<NetworkNode>();

        //    //iterate over the rows and columns and print to the console as it appears in the file
        //    //excel is not zero based!!
        //    for (int i = 1; i <= rowCount; i++)
        //    {
        //        NetworkNode n = new NetworkNode(xlRange.Cells[i, j].Value2.ToString(), xlRange.Cells[i, j + 1].Value2.ToString());
        //        nn.Add(n);
        //    }

        //public static List<NetworkNode> getNodesFromExcel()
        //{

        //    //Create COM Objects. Create a COM object for everything that is referenced
        //    Excel.Application xlApp = new Excel.Application();
        //    Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\sokhna_scheme.xlsx");
        //    Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
        //    Excel.Range xlRange = xlWorksheet.UsedRange;

        //    int rowCount = xlRange.Rows.Count;
        //    int colCount = xlRange.Columns.Count;

        //    List<NetworkNode> nn = new List<NetworkNode>();

        //    //iterate over the rows and columns and print to the console as it appears in the file
        //    //excel is not zero based!!
        //    for (int i = 1; i <= rowCount; i++)
        //    {
        //        NetworkNode n = new NetworkNode(xlRange.Cells[i, j].Value2.ToString(), xlRange.Cells[i, j + 1].Value2.ToString());
        //        nn.Add(n);
        //    }

        //    //cleanup
        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();

        //    //rule of thumb for releasing com objects:
        //    //  never use two dots, all COM objects must be referenced and released individually
        //    //  ex: [somthing].[something].[something] is bad

        //    //release com objects to fully kill excel process from running in the background
        //    Marshal.ReleaseComObject(xlRange);
        //    Marshal.ReleaseComObject(xlWorksheet);

        //    //close and release
        //    xlWorkbook.Close();
        //    Marshal.ReleaseComObject(xlWorkbook);

        //    //quit and release
        //    xlApp.Quit();
        //    Marshal.ReleaseComObject(xlApp);

        //}
    }
}