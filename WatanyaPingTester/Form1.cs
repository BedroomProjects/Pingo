using System;
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
        public Form1()
        {
            InitializeComponent();
            
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            listView1.Columns.Add("Index");
            listView1.Columns.Add("Name");
            listView1.Columns.Add("IP");
            listView1.Columns.Add("Status");
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            List<NetworkNode> ants = new List<NetworkNode>();
            NetworkNode a = new NetworkNode("Mokattam", "Anttena", "Cairo - Sokhna", "10.0.10.101");
            ants.Add(a);
            NetworkNode m1 = new NetworkNode("M1", "Anttena", "Cairo - Sokhna", "10.0.10.103");
            ants.Add(m1);
            m1 = new NetworkNode("M2", "Anttena", "Cairo - Sokhna", "10.0.10.111");
            ants.Add(m1);
            m1 = new NetworkNode("M3", "Anttena", "Cairo - Sokhna", "10.0.10.115");
            ants.Add(m1);
            
            string[] itemsArr = new string[4];
            string[] status_imgs = { "red_x.png", "green_mark.png" };
            
            ListViewItem listItem;
            int j = 1;
            for (int i = 0; i < ants.Count(); i++)
            {
                itemsArr[0] = j++.ToString();
                itemsArr[1] = ants.ElementAt(i).getName();
                itemsArr[2] = ants.ElementAt(i).getIP();
                itemsArr[3] = ants.ElementAt(i).getStatus();
                listItem = new ListViewItem(itemsArr);
                listView1.Items.Add(listItem);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
