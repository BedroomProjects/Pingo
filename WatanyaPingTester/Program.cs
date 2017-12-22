using System;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace WatanyaPingTester
{
    static class Program
    {
        static void Main(string[] args)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new IPsAsListForm());
            //Application.Run(new Cairo_Sokhna());
            //Application.Run(new CairoSokhnaDiagramForm());
            Application.Run(new StartScreen());
        }
    }
}
