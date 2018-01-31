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

        Microsoft.Office.Interop.Excel.Application oXL1;
        Microsoft.Office.Interop.Excel._Workbook oWB1;
        Microsoft.Office.Interop.Excel._Worksheet oSheet1;
        Microsoft.Office.Interop.Excel.Range oRng1;
        
        // nodeDataInfo.nodePathData 
        /*        0                        1            2                 3
         * 0     Index In Scheme        NodeName       IP        OnlinePercentage 
         * 1     Index In Scheme        NodeName       IP        OnlinePercentage
         */

        List<SchemeNode> schemeNodes;
        Thread tt;
        string resPath;
        string[] reportTimePeriod = new string[2];
        string[] reportDate = new string[2];
        List<List<string>> collectingPorts;
        NodePathFromCompany nodePathFromCompany;
        List<NodePathInfo> nodePathInfoList = new List<NodePathInfo>();
        System.Windows.Forms.Label reportLabel;

        public Reports(string resPath, List<List<string>> collectingPorts, List<SchemeNode> schemeNodes, System.Windows.Forms.Label reportLabel) {
            this.collectingPorts = collectingPorts;
            this.schemeNodes = schemeNodes;
            this.resPath = resPath;
            this.reportLabel = reportLabel;
            fillNodePathInfoList();
            reportTimePeriod[0] = DateTime.Now.ToString("HHmm", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            reportDate[0] = DateTime.UtcNow.Date.ToString("dd/MM/yyyy");
        }

        public Reports(string resPath, System.Windows.Forms.Label reportLabel) {
            this.resPath = resPath;
            this.reportLabel = reportLabel;
        } 

        public void typeReport() {
            try {
                reportLabel.Invoke((MethodInvoker)delegate {
                    reportLabel.Text = "Collecting Report Data ....";
                });
                if (tt.IsAlive) {
                    tt.Abort();
                }
            } catch (Exception e) {

            }
            for (int i = 0; i < collectingPorts.Count(); i++) {
                for (int j = 0; j < nodePathInfoList[i].nodePathData.Count(); j++) {
                    int nodeIndex = Int32.Parse(nodePathInfoList[i].nodePathData[j].ElementAt(0));
                    if (nodePathInfoList[i].nodePathData[j].Count() < 4) {
                        nodePathInfoList[i].nodePathData[j].Add(getNodeStatus(nodeIndex).ToString());
                    } else {
                        nodePathInfoList[i].nodePathData[j][3] = getNodeStatus(nodeIndex).ToString();
                    }
                }
            }
            reportTimePeriod[1] = DateTime.Now.ToString("HHmm", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            reportDate[1] = DateTime.UtcNow.Date.ToString("dd/MM/yyyy");
            tt = new Thread(createColorScaleExcel);
            tt.Start();
            //new Thread(createDetailsReport).Start();
        }

        public void fillNodePathInfoList() {
            for (int i = 0; i < collectingPorts.Count(); i++) {
                nodePathFromCompany = new NodePathFromCompany(this.collectingPorts[i], this.schemeNodes);
                nodePathFromCompany.createNodePath();
                nodePathInfoList.Add(nodePathFromCompany.getNodePathInfo());
                for (int j = 0; j < nodePathInfoList[i].nodePathData.Count(); j++) {
                    int nodeIndex = Int32.Parse(nodePathInfoList[i].nodePathData[j].ElementAt(0));
                    int cccc = nodePathInfoList[i].nodePathData[j].Count();
                    nodePathInfoList[i].nodePathData[j].Add(getNodeStatus(nodeIndex).ToString());
                }
            }
        }

        public void fillNodesStatusList() {
            
        }

        private double getNodeStatus(int nodeIndex) {
            double onlinePerc = 0.00, overallPing;
            overallPing = schemeNodes[nodeIndex].getOnlineCount() + schemeNodes[nodeIndex].getOfflineCount() + schemeNodes[nodeIndex].getTimeoutCount();
            onlinePerc = (schemeNodes[nodeIndex].getOnlineCount() / overallPing) * 100;
            return Math.Round(onlinePerc, 2);
        }

        public void createColorScaleExcel() {
            try {
                //Start Excel and get Application object.
                oXL = new Microsoft.Office.Interop.Excel.Application();
                //oXL.Visible = true;

                //Get a new workbook.
                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                oSheet.Columns["A:F"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;

                oSheet.get_Range("A1:A2", "B1:B2").Merge();
                oSheet.Cells[1, 1].Value2 = "From: " + reportDate[0] + " at " + reportTimePeriod[0] + "\nTo: " + reportDate[1] + " at " + reportTimePeriod[1];
                int m = 3;

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
                oXL.DisplayAlerts = false;
                oWB.SaveAs(resPath + ".xlsx", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                oWB.Close();
                reportLabel.Invoke((MethodInvoker)delegate {
                    reportLabel.Text = "Report is saved successfully";
                });
            } catch (Exception e) {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show(e.Message, "Reports", buttons);
            }
        }

        public void createDetailsReport() {
            try {
                //Start Excel and get Application object.
                oXL1 = new Microsoft.Office.Interop.Excel.Application();
                //oXL1.Visible = true;

                //Get a new workbook.
                oWB1 = (Microsoft.Office.Interop.Excel._Workbook)(oXL1.Workbooks.Add(""));
                oSheet1 = (Microsoft.Office.Interop.Excel._Worksheet)oWB1.ActiveSheet;

                oSheet1.PageSetup.Orientation = Excel.XlPageOrientation.xlPortrait;
                int rows = 1;
                for (int i = 0; i < schemeNodes.Count(); i++) {
                    oSheet1.get_Range("A" + rows, "B" + rows).Merge();
                    oSheet1.Cells[rows, 1].Value2 = schemeNodes[i].getNodeStatusHistory().nodeName + ":     " + schemeNodes[i].getNodeStatusHistory().nodeIP;
                    oSheet1.Cells[rows, 1].Font.Bold = true;
                    oSheet1.Cells[rows++, 1].Font.Size = 15;
                    for (int j = 0; j < schemeNodes[i].getNodeStatusHistory().NodeHistoryList.Count(); j++) {
                        oSheet1.Cells[rows, 1].Value2 = schemeNodes[i].getNodeStatusHistory().NodeHistoryList[j].status;
                        if (schemeNodes[i].getNodeStatusHistory().NodeHistoryList[j].status.Equals("Online")) {
                            oSheet1.Cells[rows, 1].Font.Color = 0x22EE22;
                        } else {
                            oSheet1.Cells[rows, 1].Font.Color = 0x000000FF;
                        }
                        oSheet1.Cells[rows, 1].Font.Bold = true;
                        oSheet1.Cells[rows, 2].Font.Bold = true;
                        oSheet1.Cells[rows++, 2].Value2 = schemeNodes[i].getNodeStatusHistory().NodeHistoryList[j].statusTimeDate;
                    }
                }

                oRng1 = oSheet.get_Range("A1", "E1");
                oRng1.EntireColumn.AutoFit();

                //oSheet.Columns["A:F"].ColumnWidth = 18;

                // oXL.Visible = false;
                oXL1.UserControl = false;
                oXL1.DisplayAlerts = false;
                oWB1.SaveAs(resPath + "Details" + ".xlsx", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                oWB1.Close();
                reportLabel.Invoke((MethodInvoker)delegate {
                    reportLabel.Text = "Details Report is saved successfully";
                });
            } catch (Exception e) {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show(e.Message, "Details Report", buttons);
            }
        }

        public void createDDetailsReport(List<NodeRecord> nodeRecordsList) {
            try {
                //Start Excel and get Application object.
                oXL1 = new Microsoft.Office.Interop.Excel.Application();
                //oXL1.Visible = true;

                //Get a new workbook.
                oWB1 = (Microsoft.Office.Interop.Excel._Workbook)(oXL1.Workbooks.Add(""));
                oSheet1 = (Microsoft.Office.Interop.Excel._Worksheet)oWB1.ActiveSheet;

                oSheet1.PageSetup.Orientation = Excel.XlPageOrientation.xlPortrait;
                int rows = 1;
                for (int i = 0; i < nodeRecordsList.Count(); i++) {
                    oSheet1.get_Range("A" + rows, "B" + rows).Merge();
                    oSheet1.Cells[rows, 1].Value2 = nodeRecordsList[i].name;
                    oSheet1.Cells[rows, 1].Font.Bold = true;
                    oSheet1.Cells[rows++, 1].Font.Size = 15;
                    for (int j = 0; j < nodeRecordsList[i].record.Count(); j++) {
                        oSheet1.Cells[rows, 1].Value2 = nodeRecordsList[i].record[j].status;
                        if (nodeRecordsList[i].record[j].status.Equals("Online")) {
                            oSheet1.Cells[rows, 1].Font.Color = 0x22EE22;
                        } else {
                            oSheet1.Cells[rows, 1].Font.Color = 0x000000FF;
                        }
                        oSheet1.Cells[rows, 1].Font.Bold = true;
                        oSheet1.Cells[rows, 2].Font.Bold = true;
                        oSheet1.Cells[rows++, 2].Value2 = nodeRecordsList[i].record[j].timeDate;
                    }
                }

                //oRng1 = oSheet.get_Range("A1", "E1");
                //oRng1.EntireColumn.AutoFit();

                //oSheet.Columns["A:F"].ColumnWidth = 18;

                // oXL.Visible = false;
                oXL1.UserControl = false;
                oXL1.DisplayAlerts = false;
                oWB1.SaveAs(resPath + "Details" + ".xlsx", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                oWB1.Close();
                reportLabel.Invoke((MethodInvoker)delegate {
                    reportLabel.Text = "Details Report is saved successfully";
                });
                
            } catch (Exception e) {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show(e.Message, "Details Report", buttons);
            }
        }
    }

    public class NodePathInfo {
        public List<List<string>> nodePathData = new List<List<string>>();
        public string portName;
    }

    public class NodeStatusHistory {
        public string nodeName, nodeIP;
        public List<StatusHistoryListItem> NodeHistoryList = new List<StatusHistoryListItem>();
    }

    public class StatusHistoryListItem {
        public string status = "", statusTimeDate = "";
    }
}
