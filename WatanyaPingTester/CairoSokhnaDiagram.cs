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
    public partial class CairoSokhnaDiagram : Form
    {
        List<NetworkNode> nodes;
        string path, greenLEDPath, redLEDPath, yellowLEDPath;
        ExcelToNode etn = new ExcelToNode();
        string fileName = "test.xlsx";
        List<string> nodesStatusList;
        static Thread t;
        bool running = false;

        public CairoSokhnaDiagram()
        {
            InitializeComponent();

            // full screen above taskbar
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;

            t = new Thread(updateStatus);

            nodes = etn.getNetworkNodes(fileName);

            try
            {
                path = Path.Combine(Environment.CurrentDirectory, @"res");
                greenLEDPath = Path.Combine(path, @"green.png");
                redLEDPath = Path.Combine(path, @"red.png");
                yellowLEDPath = Path.Combine(path, @"yellow.png");

                for (int i = 0; i < nodes.Count(); i++)
                {
                    string ipString = nodes.ElementAt(i).getIP();
                    nodes.ElementAt(i).getStatus();
                    int firstDotIndex = ipString.IndexOf(".");
                    int lastDotIndex = ipString.LastIndexOf(".") + 1;
                    int secondDotIndex = ipString.IndexOf(".", ipString.IndexOf(".") + 1);
                    int ipNetworkLength = firstDotIndex;

                    string temp = ipString.Substring(lastDotIndex);

                    if (ipString.Substring(0, firstDotIndex).Equals("10") || ipString.Equals("192.168.1.108") || ipString.Equals("192.168.1.109"))
                    {
                        var control = (PictureBox)this.GetControlByName(this, "p" + temp);
                        control.Image = Image.FromFile(redLEDPath);
                        control.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show(e.Message, "Error", buttons);
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

        private void pingBtn_Click(object sender, EventArgs e)
        {
            if (running)
            {
                try
                {
                    if (t.IsAlive)
                        t.Abort();
                }
                catch (ThreadAbortException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                running = false;
                pingBtn.Text = "Ping";
            }
            else
            {
                running = true;


                for (int i = 0; i < nodes.Count(); i++)
                {
                    string ipString = nodes.ElementAt(i).getIP();
                    nodes.ElementAt(i).getStatus();
                    int firstDotIndex = ipString.IndexOf(".");
                    int lastDotIndex = ipString.LastIndexOf(".") + 1;
                    int secondDotIndex = ipString.IndexOf(".", ipString.IndexOf(".") + 1);
                    int ipNetworkLength = firstDotIndex;

                    string temp = ipString.Substring(lastDotIndex);
                    try
                    {
                        if (nodes.ElementAt(i).isReachable())
                        {
                            if (ipString.Substring(0, firstDotIndex).Equals("10") || ipString.Equals("192.168.1.108") || ipString.Equals("192.168.1.109"))
                            {
                                var control = (PictureBox)this.GetControlByName(this, "p" + temp);
                                control.Image = Image.FromFile(greenLEDPath);
                                control.SizeMode = PictureBoxSizeMode.Zoom;
                            }

                        }

                        else
                        {

                            if (ipString.Substring(0, ipNetworkLength).Equals("10") || ipString.Equals("192.168.1.108") || ipString.Equals("192.168.1.109"))
                            {
                                var control = (PictureBox)this.GetControlByName(this, "p" + temp);
                                control.Image = Image.FromFile(redLEDPath);
                                control.SizeMode = PictureBoxSizeMode.Zoom;
                            }

                        }
                    }
                    catch (Exception r) { }
                }
                t = new Thread(updateStatus);
                t.Start();
            }


        }

        private void updateStatus()
        {
            while (running)
            {
                try
                {
                    nodesStatusList = new List<string>();
                    for (int i = 0; i < nodes.Count(); i++)
                    {
                        try
                        {
                            nodesStatusList.Add(nodes.ElementAt(i).getStatus());
                            string ipString = nodes.ElementAt(i).getIP();
                            int firstDotIndex = ipString.IndexOf(".");
                            int lastDotIndex = ipString.LastIndexOf(".") + 1;
                            string temp = ipString.Substring(lastDotIndex);
                            int secondDotIndex = ipString.IndexOf(".", ipString.IndexOf(".") + 1);
                            int ipNetworkLength = firstDotIndex;

                            if (nodes.ElementAt(i).isReachable())
                            {
                                if (ipString.Substring(0, ipNetworkLength).Equals("192") || ipString.Substring(0, ipNetworkLength).Equals("172"))
                                {
                                    continue;
                                }
                                else if (ipString.Substring(0, ipNetworkLength).Equals("10"))
                                {
                                    var control = (PictureBox)this.GetControlByName(this, "p" + temp);
                                    control.Image = Image.FromFile(greenLEDPath);
                                    control.SizeMode = PictureBoxSizeMode.Zoom;
                                }

                            }

                            else
                            {
                                if (ipString.Substring(0, ipNetworkLength).Equals("192") || ipString.Substring(0, ipNetworkLength).Equals("172"))
                                {
                                    continue;
                                }
                                else if (ipString.Substring(0, firstDotIndex).Equals("10"))
                                {
                                    var control = (PictureBox)this.GetControlByName(this, "p" + temp);
                                    control.Image = Image.FromFile(redLEDPath);
                                    control.SizeMode = PictureBoxSizeMode.Zoom;
                                }

                            }
                        }
                        catch (Exception e) { }
                    }
                    Thread.Sleep(2000);
                }
                catch (Exception ex) { }
            }
        }
    }
}
