using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace WatanyaPingTester {
    class Reports {
        Microsoft.Office.Interop.Excel.Application oXL;
        Microsoft.Office.Interop.Excel._Workbook oWB;
        Microsoft.Office.Interop.Excel._Worksheet oSheet;
        Microsoft.Office.Interop.Excel.Range oRng;
        object misvalue = System.Reflection.Missing.Value;
        ColorScale cfColorScale = null;

        // nodeDataInfo.nodePathData 
        /*        0                        1            2                 3
         * 0     Index In Scheme        NodeName       IP        OnlinePercentage 
         * 1     Index In Scheme        NodeName       IP        OnlinePercentage
         */

        List<SchemeNode> schemeNodes;
        Thread t;
        string resPath;
        string[] reportPeriod = new string[2];
        List<List<string>> collectingPorts;
        NodePathFromCompany nodePathFromCompany;
        List<NodePathInfo> nodePathInfoList = new List<NodePathInfo>();

        public Reports(string resPath, List<List<string>> collectingPorts, List<SchemeNode> schemeNodes) {
            this.collectingPorts = collectingPorts;
            this.schemeNodes = schemeNodes;
            this.resPath = resPath;
            reportPeriod[0] = DateTime.Now.ToString("HHmm", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        public void typeReport() {
            try {
                if (t.IsAlive) {
                    t.Abort();
                }
            } catch (Exception e) {

            }
            fillNodePathInfoList();
            reportPeriod[1] = DateTime.Now.ToString("HHmm", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            t = new Thread(createColorScaleExcel);
            t.Start();
        }

        public void fillNodePathInfoList() {
            for (int i = 0; i < collectingPorts.Count(); i++) {
                nodePathFromCompany = new NodePathFromCompany(this.collectingPorts[i], this.schemeNodes);
                nodePathFromCompany.createNodePath();
                nodePathInfoList.Add(nodePathFromCompany.getNodePathInfo());
                for (int j = 0; j < nodePathInfoList[i].nodePathData.Count(); j++) {
                    int nodeIndex = Int32.Parse(nodePathInfoList[i].nodePathData[j].ElementAt(0));
                    nodePathInfoList[i].nodePathData[j].Add(getNodeStatus(nodeIndex).ToString());
                }
            }
        }

        private double getNodeStatus(int nodeIndex) {
            double onlinePerc = 0.00, overallPing;
            overallPing = schemeNodes[nodeIndex].getOnlineCount() + schemeNodes[nodeIndex].getOfflineCount() + schemeNodes[nodeIndex].getTimeoutCount();
            onlinePerc = (schemeNodes[nodeIndex].getOnlineCount() / overallPing) * 100;
            return Math.Round(onlinePerc, 2);
        }

        public void createColorScaleExcel() {
            PleaseWaitForm pleaseWaitForm = new PleaseWaitForm(); 
            try {
                //Start Excel and get Application object.
                oXL = new Microsoft.Office.Interop.Excel.Application();
                //oXL.Visible = true;

                //Get a new workbook.
                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                oSheet.Columns["A:F"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;

                oSheet.get_Range("A1", "B1").Merge();
                oSheet.Cells[1, 1].Value2 = "From: " + reportPeriod[0] + " To: " + reportPeriod[1];
                int m = 2;

                for (int i = 0; i < nodePathInfoList.Count(); i++) {
                    oSheet.Cells[m, 1].Value2 = nodePathInfoList[i].portName + ":";
                    oSheet.Cells[m, 1].Font.Bold = true;
                    oSheet.Cells[m, 1].Font.Size = 20;
                    oSheet.Cells[m, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    oSheet.get_Range("A" + m.ToString(), "B" + m.ToString()).Merge();
                    // Down to top counter
                    int z = nodePathInfoList[i].nodePathData.Count() - 1;

                    for (int j = 0; j < nodePathInfoList[i].nodePathData.Count(); j++) {
                        if (j != 0 && j % 6 == 0) {
                            m += 4;
                        }
                        oSheet.Cells[m + 1, (j % 6) + 1].Value2 = nodePathInfoList[i].nodePathData[z].ElementAt(1);
                        oSheet.Cells[m + 2, (j % 6) + 1].Value2 = nodePathInfoList[i].nodePathData[z].ElementAt(2);
                        oSheet.Cells[m + 3, (j % 6) + 1].Value2 = nodePathInfoList[i].nodePathData[z].ElementAt(3);
                        z--;

                        // Set all borders for table
                        oSheet.Cells[m + 1, (j % 6) + 1].Borders.Color = System.Drawing.Color.Black.ToArgb();
                        oSheet.Cells[m + 2, (j % 6) + 1].Borders.Color = System.Drawing.Color.Black.ToArgb();
                        oSheet.Cells[m + 3, (j % 6) + 1].Borders.Color = System.Drawing.Color.Black.ToArgb();

                        // Create a color scale for third row
                        cfColorScale = (ColorScale)(oSheet.get_Range("A" + (m + 3).ToString(), "F" + (m + 3).ToString()).FormatConditions.AddColorScale(2));
                        // Min and Max color 
                        cfColorScale.ColorScaleCriteria[1].FormatColor.Color = 0x000000FF;   // Red
                        cfColorScale.ColorScaleCriteria[2].FormatColor.Color = 0x0000FF00;   // Green


                        // Set table font size and bold
                        oSheet.get_Range("A" + (m + 1).ToString(), "F" + (m + 1).ToString()).Font.Size = 11;
                        oSheet.get_Range("A" + (m + 2).ToString(), "F" + (m + 2).ToString()).Font.Size = 11;
                        oSheet.get_Range("A" + (m + 3).ToString(), "F" + (m + 3).ToString()).Font.Size = 11;

                        oSheet.get_Range("A" + (m + 1).ToString(), "F" + (m + 1).ToString()).Font.Bold = true;
                        oSheet.get_Range("A" + (m + 2).ToString(), "F" + (m + 2).ToString()).Font.Bold = true;
                        oSheet.get_Range("A" + (m + 3).ToString(), "F" + (m + 3).ToString()).Font.Bold = true;
                    }
                    m += 6;
                }

                oRng = oSheet.get_Range("A1", "E1");
                oRng.EntireColumn.AutoFit();

                oSheet.Columns["A:F"].ColumnWidth = 18;

                // oXL.Visible = false;
                oXL.UserControl = false;
                oWB.SaveAs(resPath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                oWB.Close();

            } catch (Exception e) {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show(e.Message, "Reports", buttons);
            } finally {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show("تم الانتهاء من التقرير", "Reports", buttons);
            }
        }
    }

    public class NodePathInfo {
        public List<List<string>> nodePathData = new List<List<string>>();
        public string portName;
    }
}
