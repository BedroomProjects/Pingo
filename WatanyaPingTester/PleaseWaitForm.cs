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
    public partial class PleaseWaitForm : Form
    {
        public PleaseWaitForm()
        {
            
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            InitializeComponent();
            
            string path = Path.Combine(Environment.CurrentDirectory, @"res");
            string fileName = "loading.gif";
            string picPath = Path.Combine(path, fileName);
            pictureBox1.Image = Image.FromFile(picPath);
        }
    }
}
