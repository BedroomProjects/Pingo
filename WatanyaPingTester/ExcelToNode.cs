using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;       //microsoft Excel 14 object in references-> COM tab

namespace WatanyaPingTester
{
    class ExcelToNode
    {

        public List<NetworkNode> nodes;
        public List<List<string>> result = new List<List<string>>();
        
        public void getDataFromExcel(string fileName)
        {
            // Excel File Path
            string filePath = Path.Combine(Environment.CurrentDirectory, @"data", fileName);
            nodes = new List<NetworkNode>();
            try
            {
                // Create COM Objects. Create a COM object for everything that is referenced
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                Excel.Range xlRange;

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(filePath);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlRange = xlWorkSheet.UsedRange;

                // No. of Rows and Cols in the excel sheet
                int rowCount = xlRange.Rows.Count;

                // Get the strings from each cell in the excel sheet
                // Row 1 is ignored because it contains the columns titles
                for (int i = 2; i <= rowCount; i++)
                {
                    List<string> nodeDataList = new List<string>();
                    for (int j = 1; j <= 6; j++)
                    {
                        nodeDataList.Add(xlRange.Cells[i, j].Value2.ToString());
                    }
                    nodes.Add(new NetworkNode(nodeDataList.ElementAt(0), nodeDataList.ElementAt(1), nodeDataList.ElementAt(2), nodeDataList.ElementAt(3), nodeDataList.ElementAt(4), nodeDataList.ElementAt(5)));
                    result.Add(nodeDataList);
                }

                //cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();

                //rule of thumb for releasing com objects:
                //  never use two dots, all COM objects must be referenced and released individually
                //  ex: [somthing].[something].[something] is bad

                //release com objects to fully kill excel process from running in the background
                Marshal.ReleaseComObject(xlRange);
                Marshal.ReleaseComObject(xlWorkSheet);

                //close and release
                xlWorkBook.Close();
                Marshal.ReleaseComObject(xlWorkBook);

                //quit and release
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);


                //Console.WriteLine(rowCount);
                //Console.ReadKey();
            }
            catch (FileNotFoundException e)
            {
                Console.SetCursorPosition(0, 4);
                Console.WriteLine("Error: Can not read excel file '" + filePath + "'\nCheck if file exists.");
            }
        }

        public List<List<string>> getResult(string fileName)
        {
            getDataFromExcel(fileName);
            return result;
        }

        public List<NetworkNode> getNetworkNodes(string fileName)
        {
            getDataFromExcel(fileName);
            return nodes;
        }
    }
}