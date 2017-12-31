using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace WatanyaPingTester
{
    public partial class SokhnaForm : Form
    {
        List<SchemeNode> schemeNodes = new List<SchemeNode>();

        int secondsPerPing = 2;
        bool showIPs = false, report = false;

        List<NetworkNode> nodes;
        string resPath, greenLEDPath, redLEDPath, yellowLEDPath, greyLEDPath, greennLEDPath;
        ExcelToNode etn = new ExcelToNode();
        string fileName = "sokhna_scheme.xlsx";
        //bool running = false;
        Thread t;
        StartScreen startScreen;

        public SokhnaForm( StartScreen startScreen)
        {
            InitializeComponent();
            this.CenterToScreen();
            this.startScreen = startScreen;

            // full screen above taskbar
            //this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            //this.WindowState = FormWindowState.Maximized;

            // Adding form closing event handler
            this.FormClosing += new FormClosingEventHandler(CairoAlexDiagram_Closing);

            try
            {
                resPath = Path.Combine(Environment.CurrentDirectory, @"res");
                greenLEDPath = Path.Combine(resPath, @"green.png");
                redLEDPath = Path.Combine(resPath, @"red.png");
                yellowLEDPath = Path.Combine(resPath, @"yellow.png");
                greyLEDPath = Path.Combine(resPath, @"grey1.png");
                greennLEDPath = Path.Combine(resPath, @"greenn.png");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show(e.Message, "Error", buttons);
            }
            finally
            {
                nodes = etn.getNetworkNodes(fileName);
                generateSchemeNodes(nodes);
                startThread();
            }
         
        }

        void updateNetwork()
        {
            for (int i = 0; i < schemeNodes.Count; i++)
            {
                schemeNodes[i].getNode().sendPing();
            }
        }

        void updateDisplay()
        {
            for (int i = 0; i < schemeNodes.Count; i++)
            {
                try
                {
                    if (schemeNodes.ElementAt(i).isVisible())
                    {
                        PictureBox pp = schemeNodes.ElementAt(i).getPic();
                        pp.Invoke((MethodInvoker)delegate
                        {
                            pp.Visible = true;
                        });

                        string curNodeStatus = schemeNodes[i].getNode().getStatus();
                        if (curNodeStatus == "Online")
                        {
                            setPicToGreen(schemeNodes[i].getPic());
                            updateReport(schemeNodes[i]);
                        }
                        else if (curNodeStatus == "Not Reachable")
                        {
                            setPicToYellow(schemeNodes[i].getPic());
                            updateReport(schemeNodes[i]);
                        }
                        else if (curNodeStatus == "Timeout")
                        {
                            setPicToRed(schemeNodes[i].getPic());
                            updateReport(schemeNodes[i]);
                        }
                    }
                    else
                    {
                        try
                        {
                            PictureBox pp = schemeNodes.ElementAt(i).getPic();
                            pp.Invoke((MethodInvoker)delegate
                            {
                                pp.Visible = false;
                            });
                        }
                        catch (Exception ee)
                        {

                        }
                    }
                }catch(Exception eeee){

                }
            }
        }

        void continousUpdate()
        {
            while (true)
            {
                updateNetwork();
                updateDisplay();
                Thread.Sleep(1000 * secondsPerPing);
            }
        }

        void startThread()
        {
            t = new Thread(continousUpdate);
            t.Start();
        }

        void generateSchemeNodes(List<NetworkNode> networkNodes)
        {
            string previousNodesName = "";
            comboBox1.Items.Add(new ComboboxItem("All", -1));
            for (int i = 0; i < networkNodes.Count; i++)
            {
                SchemeNode sn = new SchemeNode(networkNodes[i]);

                PictureBox cPic = (PictureBox)this.GetControlByName(this, "p" + sn.getID());
                Label cLabel = (Label)this.GetControlByName(this, "l" + sn.getID());

                // fill ComboBox
                if (!nodes.ElementAt(i).getName().Equals(previousNodesName))
                    comboBox1.Items.Add(new ComboboxItem(nodes.ElementAt(i).getName(), i));
                try
                {
                    if (cLabel != null)
                    {
                        sn.setLabel(cLabel);
                        cLabel.Text = "" + sn.getIP();
                        //cLabel.Visible = showIPs;
                        sn.setPic(cPic);
                        cPic.MouseHover += new EventHandler(pictureBoxMouseHoverEventHandler);
                        cPic.MouseLeave += new EventHandler(pictureBoxMouseLeaveEventHandler);
                    }
                }
                catch (Exception e)
                {
                    Console.Write("Exception with " + cLabel.Text);
                }
                schemeNodes.Add(sn);
                previousNodesName = nodes.ElementAt(i).getName();

            }
        }

        void setPicToGreen(PictureBox p)
        {
            try
            {
                p.Image = Image.FromFile(greenLEDPath);
                p.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        void setPicToRed(PictureBox p)
        {
            try
            {
                p.Image = Image.FromFile(redLEDPath);
                p.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        void setPicToYellow(PictureBox p)
        {
            try
            {
                p.Image = Image.FromFile(yellowLEDPath);
                p.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        void setPicToGrey(PictureBox p)
        {
            try
            {
                p.Image = Image.FromFile(greyLEDPath);
                p.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        public Control GetControlByName(Control ParentCntl, string NameToSearch)
        {
            if (ParentCntl.Name == NameToSearch)
                return ParentCntl;

            foreach (Control ChildCntl in ParentCntl.Controls)
            {
                Control ResultCntl = GetControlByName(ChildCntl, NameToSearch);
                if (ResultCntl != null)
                    return ResultCntl;
            }
            return null;
        }

        private void pictureBoxMouseHoverEventHandler(object sender, System.EventArgs e)
        {

            PictureBox p = (PictureBox)sender;
            string labelName = p.Name.Replace("p", "l");
            var control = (Label)this.GetControlByName(this, labelName);
            control.Visible = true;
        }

        private void pictureBoxMouseLeaveEventHandler(object sender, System.EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            string labelName = p.Name.Replace("p", "l");
            var control = (Label)this.GetControlByName(this, labelName);
            control.Visible = false;
        }

        private void CairoAlexDiagram_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (t.IsAlive)
                    t.Abort();
            }
            catch (Exception ee)
            {

            }
            startScreen.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < schemeNodes.Count(); i++)
            {
                schemeNodes.ElementAt(i).setVisiblility(true);
            }

            ComboboxItem item = (ComboboxItem)comboBox1.SelectedItem;
            if (item.nodeIndex != -1)
            {
                List<string> ips = new List<string>();
                ips.Add(schemeNodes.ElementAt(item.nodeIndex).getIP());
                string preIP = schemeNodes.ElementAt(item.nodeIndex).getPreviousNode();
                string ss = "";
                for (int i = 0; i < schemeNodes.Count(); i++)
                {
                    if (schemeNodes.ElementAt(i).getIP().Equals(preIP))
                    {
                        ss += preIP + " " + schemeNodes.ElementAt(i).isVisible().ToString();
                        ips.Add(preIP);
                        schemeNodes.ElementAt(i).setVisiblility(true);
                        ss += schemeNodes.ElementAt(i).isVisible().ToString() + " \n";
                        preIP = schemeNodes.ElementAt(i).getPreviousNode();
                        i = -1;
                    }
                    else if (preIP.Equals("none"))
                    {
                        break;
                    }
                }

                for (int i = 0; i < schemeNodes.Count(); i++)
                {
                    string iptemp = schemeNodes.ElementAt(i).getIP();
                    if (!ips.Contains(schemeNodes.ElementAt(i).getIP()))
                    {
                        schemeNodes.ElementAt(i).setVisiblility(false);
                    }
                }
            }
            updateDisplay();
        }

        // ComboBox item
        public class ComboboxItem
        {
            public string name;
            public int nodeIndex;

            public ComboboxItem(string name, int index)
            {
                this.name = name;
                this.nodeIndex = index;
            }

            public override string ToString()
            {
                return name;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            showIPs = !showIPs;
            foreach (SchemeNode s in schemeNodes)
            {
                try
                {
                    s.getLabel().Visible = showIPs;
                }catch(Exception aa){

                }
            }
        }

        private void reportBtn_Click(object sender, EventArgs e)
        {
            if (report)
            {
                report = false;
                string temp = getReport();
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show(temp, "Report", buttons);
                reportLED.Image = Image.FromFile(greyLEDPath);
                reportLED.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                report = true;
                reportLED.Image = Image.FromFile(greennLEDPath);
                reportLED.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private string getReport()
        {
            string result = "";
            double onlinePerc, offlinePerc, timeoutPerc, overallPing;
            for (int i = 0; i < schemeNodes.Count(); i++ )
            {
                overallPing = schemeNodes[i].getOnlineCount() + schemeNodes[i].getOfflineCount() + schemeNodes[i].getTimeoutCount();
                onlinePerc = (schemeNodes[i].getOnlineCount() / overallPing) * 100;
                offlinePerc = (schemeNodes[i].getOfflineCount() / overallPing) * 100;
                timeoutPerc = (schemeNodes[i].getTimeoutCount() / overallPing) * 100;
                if (overallPing == 0) continue;
                result += "C: " + overallPing.ToString() + " || ";
                result += schemeNodes[i].getName();
                result += " ----- ";
                result = result 
                    + "Online: " + Math.Round(onlinePerc, 2).ToString()
                    + ", Offline: " + Math.Round(offlinePerc, 2).ToString()
                    + ", Timeout: " + Math.Round(timeoutPerc, 2).ToString()
                    + "\n";
            }
            return result;
        }

        private void updateReport(SchemeNode schNode)
        {
            if (report)
            {
                string curNodeStatus = schNode.getNode().getStatus();
                if (curNodeStatus == "Online")
                {
                    schNode.incrementOnline();
                }
                else if (curNodeStatus == "Not Reachable")
                {
                    schNode.incrementOffline();
                }
                else if (curNodeStatus == "Timeout")
                {
                    schNode.incrementTimeout();
                }
            }
            else
            {
                return;
            }
        }
    }
}
