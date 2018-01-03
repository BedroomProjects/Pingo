using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatanyaPingTester
{
    class CreateReports
    {
        Microsoft.Office.Interop.Excel.Application oXL;
        Microsoft.Office.Interop.Excel._Workbook oWB;
        Microsoft.Office.Interop.Excel._Worksheet oSheet;
        Microsoft.Office.Interop.Excel.Range oRng;
        object misvalue = System.Reflection.Missing.Value;
        List<List<string>> reportList = new List<List<string>>();
        string resPath;

        public CreateReports(string resPath, List<List<string>> reportList)
        {
            this.resPath = resPath;
            this.reportList = reportList;
        }

        public void createColorScaleData()
        {
            try
            {
                //Start Excel and get Application object.
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = true;

                //Get a new workbook.
                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                //Add table headers going cell by cell.
                oSheet.Cells[1, 1] = "Name";
                oSheet.Cells[1, 2] = "IP";
                oSheet.Cells[1, 3] = "Online";
                oSheet.Cells[1, 4] = "Not Reachable";
                oSheet.Cells[1, 5] = "Timeout";

                //Format A1:E1 as bold, vertical alignment = center.
                oSheet.get_Range("A1", "E1").Font.Bold = true;
                oSheet.get_Range("A1", "E1").VerticalAlignment =
                Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                for (int i = 0; i < reportList.Count(); i++)
                {
                    for (int j = 0; j < reportList[0].Count(); j++)
                    {
                        oSheet.Cells[i + 2, j + 1].Value2 = reportList[i].ElementAt(j);
                    }
                }
                oRng = oSheet.get_Range("A1", "E1");
                oRng.EntireColumn.AutoFit();

                oXL.Visible = false;
                oXL.UserControl = false;
                oWB.SaveAs(resPath + "\\sokhnaReport.xlsx", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                oWB.Close();

            }
            catch (Exception e)
            {

            }
        }

        public void createChartTable()
        {
            try
            {
                //Start Excel and get Application object.
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = true;

                //Get a new workbook.
                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                //Add table headers going cell by cell.
                oSheet.Cells[1, 1] = "Name";
                oSheet.Cells[1, 2] = "IP";
                oSheet.Cells[1, 3] = "Online";
                oSheet.Cells[1, 4] = "Not Reachable";
                oSheet.Cells[1, 5] = "Timeout";

                //Format A1:E1 as bold, vertical alignment = center.
                oSheet.get_Range("A1", "E1").Font.Bold = true;
                oSheet.get_Range("A1", "E1").VerticalAlignment =
                Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                for (int i = 0; i < reportList.Count(); i++)
                {
                    for (int j = 0; j < reportList[0].Count(); j++)
                    {
                        oSheet.Cells[i + 2, j + 1].Value2 = reportList[i].ElementAt(j);
                    }
                }
                oRng = oSheet.get_Range("A1", "E1");
                oRng.EntireColumn.AutoFit();

                oXL.Visible = false;
                oXL.UserControl = false;
                oWB.SaveAs(resPath + "\\sokhnaReport.xlsx", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                oWB.Close();

            }
            catch (Exception e)
            {

            }
        }
    }
}
