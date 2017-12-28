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
        PleaseWaitForm pleaseWait = new PleaseWaitForm(); // Display form modelesslys
        
        public StartScreen()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.CenterToScreen();
        }
        

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void sokhnaBtn_Click(object sender, EventArgs e)
        {
            //pleaseWait.Show(); //  ALlow main UI thread to properly display please wait form.
            Application.DoEvents(); //CairoSokhnaDiagramForm csd = new CairoSokhnaDiagramForm(this);
            SokhnaForm csd = new SokhnaForm(this);
            csd.Show();
            this.Hide();
            //pleaseWait.Hide();
        }

        private void allRoadsBtn_Click(object sender, EventArgs e)
        {
            //pleaseWait.Show(); //  ALlow main UI thread to properly display please wait form.
            Application.DoEvents();
            IPsAsListForm ipForm = new IPsAsListForm(this);
            ipForm.Show();
            this.Hide();
            //pleaseWait.Hide();
        }

        private void alexBtn_Click(object sender, EventArgs e)
        {
            //pleaseWait.Show();

            //  ALlow main UI thread to properly display please wait form.
            Application.DoEvents();
            //CairoAlexDiagramForm cad = new CairoAlexDiagramForm(this);
            AlexForm cad = new AlexForm(this);
            cad.Show();
            this.Hide();
            //pleaseWait.Hide();
        }
    }
}
