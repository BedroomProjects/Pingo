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
    public partial class StartScreen : Form
    {
        public StartScreen()
        {
            InitializeComponent();
        }

        

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void sokhnaBtn_Click(object sender, EventArgs e)
        {
            CairoSokhnaDiagramForm csd = new CairoSokhnaDiagramForm();
            csd.Show();
            this.Hide();
        }

        private void allRoadsBtn_Click(object sender, EventArgs e)
        {
            IPsAsListForm ipForm = new IPsAsListForm();
            ipForm.Show();
            this.Hide();
        }

        private void alexBtn_Click(object sender, EventArgs e)
        {
            CairoAlexDiagramForm cad = new CairoAlexDiagramForm();
            cad.Show();
            this.Hide();
        }
    }
}
