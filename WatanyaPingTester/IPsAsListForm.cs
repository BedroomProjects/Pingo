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
    public partial class IPsAsListForm : Form
    {
        ImageList imageList = new ImageList();
        string[] status_imgs = { "red_x.png", "green_mark.png" };
        List<NetworkNode> nodes;
        string path, markPath, xPath;
        ExcelToNode etn = new ExcelToNode();
        string fileName = "test.xlsx";
        string[] excelSheetsNames = { "test.xlsx", "alex_scheme.xlsx" };
        List<string> nodesStatusList;
        static Thread t;
        string selectedItem;
        bool running = false;
        StartScreen startScreen;
        public IPsAsListForm(StartScreen startScreen)
        {
            InitializeComponent();
            this.startScreen = startScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            ping2.Enabled = false;

            this.FormClosing += new FormClosingEventHandler(Form_Closing_Handler);
            // Data

            t = new Thread(updateStatus);
            
            try {
                path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                path += "\\..\\..\\res";
                markPath = path + "\\green_mark.ico";
                xPath = path + "\\red_x.ico";
                string[] imageFiles = Directory.GetFiles(@path);
                foreach (var file in imageFiles)
                {
                    //Add images to Imagelist
                    imageList.Images.Add(Image.FromFile(file));
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
        
        private void button1_Click(object sender, EventArgs e)
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
                        nodes.ElementAt(i).sendPing();
                        nodesStatusList.Add(nodes.ElementAt(i).getStatus());

                        Image img;
                        if (nodes.ElementAt(i).isReachable())
                            img = Image.FromFile(@markPath);
                        else
                            img = Image.FromFile(@xPath);
                        gridView1.Rows[i].Cells[3].Value = nodesStatusList.ElementAt(i);
                        gridView1.Rows[i].Cells[4].Value = img;
                    }
                    Thread.Sleep(2000);
                }catch(Exception ex){
                    break;
                }
            }
        }


        private void ping2_Click(object sender, EventArgs e)
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
                ping2.Text = "Ping";
            }
            else
            {
                running = true;
                ping2.Text = "Stop!";
                gridView1.Rows.Clear();
                if (selectedItem.Equals("Cairo - Sokhna"))
                {
                    nodes = etn.getNetworkNodes(excelSheetsNames[0]);
                }
                else if (selectedItem.Equals("Cairo - Alexandria"))
                {
                    nodes = etn.getNetworkNodes(excelSheetsNames[1]);
                }
                var row = new string[4];
                int j = 1;

                for (int i = 0; i < nodes.Count(); i++)
                {
                    nodes.ElementAt(i).sendPing();
                    row[0] = j++.ToString();
                    row[1] = nodes.ElementAt(i).getName();
                    row[2] = nodes.ElementAt(i).getIP();
                    row[3] = nodes.ElementAt(i).getStatus();

                    Image img;
                    if (nodes.ElementAt(i).isReachable())
                        img = Image.FromFile(@markPath);
                    else
                        img = Image.FromFile(@xPath);

                    gridView1.Rows.Add(row);
                    gridView1.Rows[i].Cells[4].Value = img;
                }
                t = new Thread(updateStatus);
                t.Start();
                //ping2.Text = "Ping";
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = comboBox2.SelectedItem.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (t.IsAlive)
                    t.Abort();
            }
            catch (ThreadAbortException ex)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show(ex.Message, "Error", buttons);
            }
            //gridView1.Rows.Clear();
            ping2.Enabled = true;
            selectedItem = comboBox1.SelectedItem.ToString();
            
        }

        private void Form_Closing_Handler(object sender, System.EventArgs e)
        {
            startScreen.Show();
        }

    }
}
