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
    public partial class AlexForm : Form
    {
        List<SchemeNode> schemeNodes = new List<SchemeNode>();

        // Timers
        private System.Windows.Forms.Timer alexTimer;
        private int pingTimerCounter = 1, reportTimerCounter = 25;    // seconds

        Reports reportObject;

        int secondsPerPing = 1;
        bool showIPs = false, report = false;
        List<NetworkNode> nodes;

        private string[] collectingPortArr = { "19 El Sheikh Zaid Gate", "20 Road El Farag Gate", "21 Cairo Gate", "29 Abo Rawash Gate", "24 Chillout", "18 6 October Gate", "41 Sadat 1 Gate", "42 Sadat 2 Gate", "48 Al Alamin Gate", "35 Eqlimy Gate", "55 Alexandria Gate" };
        private List<List<string>> collectingPortsList = new List<List<string>>();

        List<List<string>> reportList = new List<List<string>>();
        string path, greenLEDPath, redLEDPath, yellowLEDPath, greyLEDPath, greennLEDPath;
        ExcelToNode etn = new ExcelToNode();
        string fileName = "alex_scheme_updated_Copy.xlsx";
        //bool running = false;
        Thread t;
        StartScreen startScreen;

        PleaseWaitForm pleaseWait = new PleaseWaitForm();

        public AlexForm(StartScreen startScreen)
        {
            InitializeComponent();
            this.startScreen = startScreen;
            this.CenterToScreen();
            // full screen above taskbar
            //this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            //this.WindowState = FormWindowState.Maximized;

            intializeTimers();

            // Adding form closing event handler
            this.FormClosing += new FormClosingEventHandler(CairoAlexDiagram_Closing);
            fillCollectingPortsList();
            try
            {
                path = Path.Combine(Environment.CurrentDirectory, @"res");
                greenLEDPath = Path.Combine(path, @"green.png");
                redLEDPath = Path.Combine(path, @"red.png");
                yellowLEDPath = Path.Combine(path, @"yellow.png");
                greyLEDPath = Path.Combine(path, @"grey1.png");
                greennLEDPath = Path.Combine(path, @"greenn.png");
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

        void intializeTimers()
        {
            // sokhnaTimer intialization
            alexTimer = new System.Windows.Forms.Timer();
            alexTimer.Tick += new EventHandler(sokhnaTimer_Tick);
            alexTimer.Interval = 1000; // 1 second
            alexTimer.Start();
        }

        private void sokhnaTimer_Tick(object sender, EventArgs e)
        {
            pingTimerCounter--;

            if (report)
            {
                reportTimerCounter--;
                if (reportTimerCounter == 0)
                {
                    reportTimerCounter = 180;
                    reportObject.typeReport();
                }
            }
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

        public void fillCollectingPortsList()
        {
            List<string> portData;
            for (int i = 0; i < collectingPortArr.Length; i++)
            {
                portData = new List<string>();
                string[] tempArr = collectingPortArr[i].Split(' ');
                // Add index of port
                portData.Add(tempArr[0]);
                string restOfString = "";
                for (int j = 1; j < tempArr.Length; j++)
                {
                    restOfString += tempArr[j] + " ";
                }
                // Add port name
                portData.Add(restOfString);
                collectingPortsList.Add(portData);
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
                }
                catch (Exception eeee)
                {

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

        private void pictureBoxMouseHoverEventHandler(object sender, System.EventArgs e)
        {
            if (!showIPs)
            {
                PictureBox p = (PictureBox)sender;
                string labelName = p.Name.Replace("p", "l");
                var control = (Label)this.GetControlByName(this, labelName);
                control.Visible = true;
            }
        }

        private void pictureBoxMouseLeaveEventHandler(object sender, System.EventArgs e)
        {
            if (!showIPs)
            {
                PictureBox p = (PictureBox)sender;
                string labelName = p.Name.Replace("p", "l");
                var control = (Label)this.GetControlByName(this, labelName);
                control.Visible = false;
            }
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
                }
                catch (Exception aa)
                {

                }
            }
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

        void showLoading()
        {
            Thread t = new Thread(pleaseWait.Show);
        }

        private void reportBtn_Click(object sender, EventArgs e)
        {
            if (report)
            {
                report = false;

                reportLED.Image = Image.FromFile(greyLEDPath);
                reportLED.SizeMode = PictureBoxSizeMode.Zoom;
                reportObject.typeReport();
            }
            else
            {
                report = true;
                reportLED.Image = Image.FromFile(greennLEDPath);
                reportLED.SizeMode = PictureBoxSizeMode.Zoom;
                reportObject = new Reports(path + "\\alexColorReport.xlsx", collectingPortsList, schemeNodes, reportStatusLabel);
            }
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