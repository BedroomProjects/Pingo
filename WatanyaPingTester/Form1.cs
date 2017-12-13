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


namespace WatanyaPingTester
{
    public partial class Form1 : Form
    {
        ImageList imageList = new ImageList();
        string[] status_imgs = { "red_x.png", "green_mark.png" };
        List<NetworkNode> ants;
        string path, markPath, xPath;

        public Form1()
        {
            InitializeComponent();

            // Data
            ants = new List<NetworkNode>();
            NetworkNode a = new NetworkNode("Mokattam", "Anttena", "Cairo - Sokhna", "10.0.10.101");
            ants.Add(a);
            NetworkNode m1 = new NetworkNode("M1", "Anttena", "Cairo - Sokhna", "10.0.10.103");
            ants.Add(m1);
            m1 = new NetworkNode("M2", "Anttena", "Cairo - Sokhna", "10.0.10.111");
            ants.Add(m1);
            m1 = new NetworkNode("M3", "Anttena", "Cairo - Sokhna", "10.0.10.115");
            ants.Add(m1);
            
            
            try {
                //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\Names.txt");
                path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                path += "\\..\\..\\res";
                markPath = path + "\\green_mark.ico";
                xPath = path + "\\red_x.ico";

                //MessageBoxButtons buttons = MessageBoxButtons.OK;
                //DialogResult result;
                //// Displays the MessageBox.
                //result = MessageBox.Show(markPath, "Error", buttons);

                
                
                //string[] imageFiles = Directory.GetFiles(@"G:\Fahim\New folder\Pingo\WatanyaPingTester\res");
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

        
        private void ping2_Click(object sender, EventArgs e)
        {
            gridView1.Rows.Clear();
            var row = new string[4];
            int j = 1;
            for (int i = 0; i < ants.Count(); i++)
            {
                row[0] = j++.ToString();
                row[1] = ants.ElementAt(i).getName();
                row[2] = ants.ElementAt(i).getIP();
                row[3] = ants.ElementAt(i).getStatus();
                
                Image img;
                if(ants.ElementAt(i).isReachable())
                    img = Image.FromFile(@markPath);
                else
                    img = Image.FromFile(@xPath);

                gridView1.Rows.Add(row);
                gridView1.Rows[i].Cells[4].Value = img;
            }
        }
    }
}
