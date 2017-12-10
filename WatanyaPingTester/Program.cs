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
            /*
             * 
             * // MOKATTAM
            NetworkNode a = new NetworkNode("Mokattam Ant. 1", "10.0.10.101");
            ants.Add(a);

            a = new NetworkNode("Mokattam Ant. 2", "10.0.10.102");
            ants.Add(a);

            // M1
            a = new NetworkNode("M1 Swtich", "192.168.1.160");
            ants.Add(a);

            a = new NetworkNode("M1 Ant. 1", "10.0.10.103");
            ants.Add(a);

            a = new NetworkNode("M1 Ant. 2", "10.0.10.104");
            ants.Add(a);

            a = new NetworkNode("M1 Ant. 3", "10.0.10.106");
            ants.Add(a);

            a = new NetworkNode("M1 Ant. 4", "10.0.10.108");
            ants.Add(a);

            a = new NetworkNode("M1 Ant. 5", "10.0.10.110");
            ants.Add(a);
            
            // DEGLA
            a = new NetworkNode("Degla Swtich", "10.0.10.193");
            ants.Add(a);

            a = new NetworkNode("Degla Ant. 1", "10.0.10.107");
            ants.Add(a);

            // CAIRO
            a = new NetworkNode("Cairo Swtich", "192.168.1.10");
            ants.Add(a);

            a = new NetworkNode("Cairo Ant. 1", "192.168.1.109");
            ants.Add(a);

            a = new NetworkNode("Cairo Ant. 2", "10.0.10.105");
            ants.Add(a);

            // M2
            a = new NetworkNode("M2 Switch", "192.168.1.161");
            ants.Add(a);

            a = new NetworkNode("M2 Ant. 1", "10.0.10.111");
            ants.Add(a);

            a = new NetworkNode("M2 Ant. 2", "10.0.10.112");
            ants.Add(a);

            a = new NetworkNode("M2 Ant. 3", "10.0.10.114");
            ants.Add(a);

            // TOHAMY
            a = new NetworkNode("Tohamy Switch", "10.0.10.113");
            ants.Add(a);

            a = new NetworkNode("Tohamy Ant. 1", "10.0.10.113");
            ants.Add(a);

            // M3
            a = new NetworkNode("M3 Switch", "192.168.1.162");
            ants.Add(a);

            a = new NetworkNode("M3 Ant. 1", "10.0.10.115");
            ants.Add(a);

            a = new NetworkNode("M3 Ant. 2", "10.0.10.116");
            ants.Add(a);

            a = new NetworkNode("M3 Ant. 3", "10.0.10.118");
            ants.Add(a);

            // Gendaly 1
            a = new NetworkNode("Gendaly 1 Switch", "192.168.1.184");
            ants.Add(a);

            a = new NetworkNode("Gendaly 1 Ant. 1", "10.0.10.117");
            ants.Add(a);

            // Kilo 61
            a = new NetworkNode("Kilo 61", "10.0.10.143");
            ants.Add(a);

            updateNetwork();
            updateDisplay();*/

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
