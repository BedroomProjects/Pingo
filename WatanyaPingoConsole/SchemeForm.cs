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
using System.Runtime.InteropServices;
using System.Threading;

namespace WatanyaPingoConsole
{
    public partial class SchemeForm : Form
    {

        int secondsPerPing = 1;
        bool showIPs = false;

        List<SchemeNode> schemeNodes = new List<SchemeNode>();
        public SchemeForm(List<NetworkNode> networkNodes)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            generateSchemeNodes(networkNodes);
            startThread();
        }

        void setPicToGreen(PictureBox p)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, @"LEDs\green-icon.bmp");
            try
            {
                p.Image = Image.FromFile(filePath);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        void setPicToRed(PictureBox p)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, @"LEDs\red-icon.bmp");
            try
            {
                p.Image = Image.FromFile(filePath);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        void setPicToYellow(PictureBox p)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, @"LEDs\yellow-icon.bmp");
            try
            {
                p.Image = Image.FromFile(filePath);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        void setPicToLoading(PictureBox p)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, @"LEDs\loading.gif");
            try
            {
                p.Image = Image.FromFile(filePath);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        void generateSchemeNodes(List<NetworkNode> networkNodes)
        {
            for (int i = 0; i < networkNodes.Count; i++) {
                
                SchemeNode sn = new SchemeNode(networkNodes[i]);

                Label cLabel = (Label) this.Controls.OfType<Label>().FirstOrDefault(c=>c.Name == "l" + sn.getID());
                PictureBox cPic = (PictureBox) this.Controls.OfType<PictureBox>().FirstOrDefault(c => c.Name == "p" + sn.getID());
                try
                {
                    if (cLabel != null)
                    {
                        sn.setLabel(cLabel);
                        cLabel.Text = "" + sn.getIP();
                        cLabel.Visible = showIPs;
                        sn.setPic(cPic);
                        cPic.MouseHover += new EventHandler(pictureBoxMouseHoverEventHandler);
                        cPic.MouseLeave += new EventHandler(pictureBoxMouseLeaveEventHandler);
                        //cLabel.Visible = false;
                        //cLabel.Invoke((MethodInvoker)(() => cLabel.Text = "ID: " + sn.getID()));
                    }
                }
                catch (Exception e)
                {
                    Console.Write("Exception with " + cLabel.Text);
                }
                setPicToLoading(sn.getPic());
                schemeNodes.Add(sn);

            }
        }

        void updateNetwork()
        {
            for (int i = 0; i < schemeNodes.Count; i++)
            {
                schemeNodes[i].getNode().sendPing();
            }
        }

        void updateDisplay()
        {
            for (int i = 0; i < schemeNodes.Count; i++)
            {
                string curNodeStatus= schemeNodes[i].getNode().getStatus();
                if (curNodeStatus == "Online")
                {
                    setPicToGreen(schemeNodes[i].getPic());
                    schemeNodes[i].getLabel().ForeColor = Color.LightGreen;
                }
                else if (curNodeStatus == "Offline")
                {
                    setPicToRed(schemeNodes[i].getPic());
                    schemeNodes[i].getLabel().ForeColor = Color.LightPink;
                }
                else if (curNodeStatus == "Timeout")
                {
                    setPicToYellow(schemeNodes[i].getPic());
                    schemeNodes[i].getLabel().ForeColor = Color.Yellow;
                }
            }
        }

        void continousUpdate()
        {
            while (true)
            {
                updateNetwork();
                updateDisplay();
                Thread.Sleep(1000 * secondsPerPing);
            }
        }
        void startThread()
        {
            Thread t = new Thread(continousUpdate);
            t.Start();
        }

        private void pictureBoxMouseHoverEventHandler(object sender, System.EventArgs e)
        {
            if (!showIPs)
            {
                PictureBox p = (PictureBox)sender;
                string labelName = p.Name.Replace("p", "l");
                var control = this.Controls.OfType<Label>().FirstOrDefault(c => c.Name == labelName);
                //var control = (Label)this.GetControlByName(this, labelName);
                control.Visible = true;
            }
        }

        private void pictureBoxMouseLeaveEventHandler(object sender, System.EventArgs e)
        {
            if (!showIPs)
            {
                PictureBox p = (PictureBox)sender;
                string labelName = p.Name.Replace("p", "l");
                var control = this.Controls.OfType<Label>().FirstOrDefault(c => c.Name == labelName);
                //var control = (Label)this.GetControlByName(this, labelName);
                control.Visible = false;
            }
        }

        /// /// /// ///

        private void Form1_Load(object sender, EventArgs e)
        {
            AllocConsole();
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label48_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label47_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label42_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            showIPs = !checkBox1.Checked;
                for (int i = 0; i < schemeNodes.Count; i++)
                {
                    schemeNodes[i].getLabel().Visible = !checkBox1.Checked;
                }
        }

    }
}
