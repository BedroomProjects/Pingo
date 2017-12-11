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

            label2.Text = ants.ElementAt(0).getStatus();
            label4.Text = ants.ElementAt(1).getStatus();
            label6.Text = ants.ElementAt(2).getStatus();
            label8.Text = ants.ElementAt(3).getStatus();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
