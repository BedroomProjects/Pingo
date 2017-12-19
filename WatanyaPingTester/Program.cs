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
        static List<NetworkNode> ants = new List<NetworkNode>();

        //static void sendPing()
        //{
        //    string ip = "198.162.0.101";
        //    Ping ping = new Ping();
        //    PingReply rp;
        //    rp = ping.Send(ip);
        //    if (rp.Status == IPStatus.Success)
        //        Console.WriteLine("Yos");
        //    else
        //        Console.WriteLine("Nop");
        //    Console.WriteLine("Done.");
        //}

        static void displayAntennaStatus(NetworkNode a)
        {
            Console.WriteLine(a.getName() + " (" + a.getIP() + "): " + a.getStatus());
        }

        static void updateNetwork()
        {
            for (int i = 0; i < ants.Count; i++)
            {
                //ants[i].ping();
            }
        }

        static void updateDisplay()
        {
            for (int i = 0; i < ants.Count; i++)
            {
                displayAntennaStatus(ants[i]);
            }
        }

        static void Main(string[] args)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //Application.Run(new Cairo_Sokhna());
            Application.Run(new Cairo_Sokhna());
        }
    }
}
