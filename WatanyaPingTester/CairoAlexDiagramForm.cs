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
    public partial class CairoAlexDiagramForm : Form
    {
        List<SchemeNode> schemeNodes = new List<SchemeNode>();

        int secondsPerPing = 1;
        bool showIPs = false;

        List<NetworkNode> nodes;
        string path, greenLEDPath, redLEDPath, yellowLEDPath, greyLEDPath;
        ExcelToNode etn = new ExcelToNode();
        string fileName = "alex_scheme.xlsx";
        //bool running = false;
        Thread t;
        StartScreen startScreen;
        
        public CairoAlexDiagramForm(StartScreen startScreen)
        {
            InitializeComponent();
            this.startScreen = startScreen;

            // full screen above taskbar
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;

            // Adding form closing event handler
            this.FormClosing += new FormClosingEventHandler(CairoAlexDiagram_Closing);

            try
            {
                path = Path.Combine(Environment.CurrentDirectory, @"res");
                greenLEDPath = Path.Combine(path, @"green.png");
                redLEDPath = Path.Combine(path, @"red.png");
                yellowLEDPath = Path.Combine(path, @"yellow.png");
                greyLEDPath = Path.Combine(path, @"grey1.png");
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
            }
        }

        void generateSchemeNodes(List<NetworkNode> networkNodes)
        {
            for (int i = 0; i < networkNodes.Count; i++)
            {
                SchemeNode sn = new SchemeNode(networkNodes[i]);

                PictureBox cPic = (PictureBox)this.GetControlByName(this, "p" + sn.getID());
                Label cLabel = (Label)this.GetControlByName(this, "l" + sn.getID());
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
                setPicToGrey(cPic);
                schemeNodes.Add(sn);

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
            if (t.IsAlive)
                t.Abort();
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

        private void pingBtn_Click(object sender, EventArgs e)
        {
            startThread();
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
                string curNodeStatus = schemeNodes[i].getNode().getStatus();
                if (curNodeStatus == "Online")
                {
                    setPicToGreen(schemeNodes[i].getPic());
                    schemeNodes[i].getLabel().ForeColor = Color.LightGreen;
                }
                else if (curNodeStatus == "Not Reachable")
                {
                    setPicToRed(schemeNodes[i].getPic());
                    schemeNodes[i].getLabel().ForeColor = Color.LightPink;
                }
                else if (curNodeStatus == "Timeout")
                {
                    setPicToYellow(schemeNodes[i].getPic());
                    schemeNodes[i].getLabel().ForeColor = Color.Yellow;
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
    }
}