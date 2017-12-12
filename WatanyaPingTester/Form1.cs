using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WatanyaPingTester
{
    public partial class Form1 : Form
    {
        ImageList imageList = new ImageList();
        string[] status_imgs = { "red_x.png", "green_mark.png" };
        List<NetworkNode> ants;

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
                string[] imageFiles = Directory.GetFiles(@"G:\Fahim\Pingo\WatanyaPingTester\res");
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
            var row = new string[4];
            int j = 1;
            for (int i = 0; i < ants.Count(); i++)
            {
                row[0] = j++.ToString();
                row[1] = ants.ElementAt(i).getName();
                row[2] = ants.ElementAt(i).getIP();
                row[3] = ants.ElementAt(i).getStatus();
                
                //DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                Image img;
                //Icon img = new Icon(this.GetType(), @"G:\Fahim\Pingo\WatanyaPingTester\res\green_mark.ico");
                if(ants.ElementAt(i).isReachable())
                    img = Image.FromFile(@"G:\Fahim\Pingo\WatanyaPingTester\res\green_mark.ico");
                else
                    img = Image.FromFile(@"G:\Fahim\Pingo\WatanyaPingTester\res\red_x.ico");

                //pic.Image = img;
                gridView1.Rows.Add(row);
                gridView1.Rows[i].Cells[4].Value = img;
            }
        }
    }
}
