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
    public partial class Cairo_Sokhna : Form
    {
        List<NetworkNode> nodes;
        string path, markPath, xPath;
        ExcelToNode etn = new ExcelToNode();
        string fileName = "test.xlsx";
        List<string> nodesStatusList;
        static Thread t;
        string selectedItem;
        bool running = false;

        public Cairo_Sokhna()
        {
            InitializeComponent();
            // Makes form appear in normal size
            this.WindowState = FormWindowState.Normal;
            // Makes form appear with no upper bar
            //this.FormBorderStyle = FormBorderStyle.None;
            // Makes form appear in full screen
            //this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            t = new Thread(updateStatus);

            try
            {
                path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                path += "\\..\\..\\res";
                markPath = path + "\\green_mark.ico";
                xPath = path + "\\red_x.ico";
                string[] imageFiles = Directory.GetFiles(@path);
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
                nodes = etn.getNetworkNodes(fileName);

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
                            if (ipString.Substring(0, ipNetworkLength).Equals("192") || ipString.Substring(0, ipNetworkLength).Equals("172"))
                            {
                                var control = this.Controls.OfType<Label>()
                                           .FirstOrDefault(c => c.Name == "s" + temp);
                                control.Text = nodes.ElementAt(i).getIP();
                                control.ForeColor = Color.Black;
                                control.BackColor = Color.Lime;
                            }
                            else if (ipString.Substring(0, firstDotIndex).Equals("10"))
                            {
                                var control = this.Controls.OfType<Label>()
                                           .FirstOrDefault(c => c.Name == "a" + temp);
                                control.Text = nodes.ElementAt(i).getIP();
                                control.ForeColor = Color.Black;
                                control.BackColor = Color.Lime;
                            }

                        }

                        else
                        {

                            if (ipString.Substring(0, ipNetworkLength).Equals("192") || ipString.Substring(0, ipNetworkLength).Equals("172"))
                            {
                                var control = this.Controls.OfType<Label>()
                                           .FirstOrDefault(c => c.Name == "s" + temp);
                                control.Text = nodes.ElementAt(i).getIP();
                                control.ForeColor = Color.White;
                                control.BackColor = Color.Red;
                            }
                            else if (ipString.Substring(0, ipNetworkLength).Equals("10"))
                            {
                                var control = this.Controls.OfType<Label>()
                                           .FirstOrDefault(c => c.Name == "a" + temp);
                                control.Text = nodes.ElementAt(i).getIP();
                                control.ForeColor = Color.White;
                                control.BackColor = Color.Red;
                            }

                        }
                    }
                    catch (Exception r) { }
                }
                //pingBtn.Text = "Stop!";
                t = new Thread(updateStatus);
                t.Start();
            }
        }

        private void a100_Click(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void updateStatus()
        {
            while (true)
            {
                try
                {
                    nodesStatusList = new List<string>();
                    for (int i = 0; i < nodes.Count(); i++)
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
                                var control = this.Controls.OfType<Label>()
                                           .FirstOrDefault(c => c.Name == "s" + temp);
                                control.ForeColor = Color.Black;
                                control.BackColor = Color.Lime;
                            }
                            else if (ipString.Substring(0, ipNetworkLength).Equals("10"))
                            {
                                var control = this.Controls.OfType<Label>()
                                           .FirstOrDefault(c => c.Name == "a" + temp);
                                control.ForeColor = Color.Black;
                                control.BackColor = Color.Lime;
                            }

                        }

                        else {
                            if (ipString.Substring(0, ipNetworkLength).Equals("192") || ipString.Substring(0, ipNetworkLength).Equals("172"))
                            {
                                var control = this.Controls.OfType<Label>()
                                           .FirstOrDefault(c => c.Name == "s" + temp);
                                control.ForeColor = Color.White;
                                control.BackColor = Color.Red;
                            }
                            else if (ipString.Substring(0, firstDotIndex).Equals("10"))
                            {
                                var control = this.Controls.OfType<Label>()
                                           .FirstOrDefault(c => c.Name == "a" + temp);
                                control.ForeColor = Color.White;
                                control.BackColor = Color.Red;
                            }
 
                        }
                    }
                    Thread.Sleep(20000);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            running = false;
            t.Abort();
            this.Close();
        }
    }
}
