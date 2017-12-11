using System;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace WatanyaPingoConsole
{

    static class ConsoleProgram
    {
        public static int timePerPing = 3;
        static List<NetworkNode> ants = new List<NetworkNode>();


        static void displayAntennaStatus(NetworkNode a)
        {
            Console.Write(a.getName() + " (" + a.getIP() + "): ");
            Console.SetCursorPosition(35, Console.CursorTop);
            if (a.getStatus() == "Success")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(a.getStatus());
            }
            else if (a.getStatus() == "Fail")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(a.getStatus());
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(a.getStatus());
            }
            Console.ResetColor();
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        static void updateNetwork()
        {
            for (int i = 0; i < ants.Count; i++)
            {
                ants[i].sendPing();
            }
        }

        static void updateNetworkAndDisplay()
        {
            for (int i = 0; i < ants.Count; i++)
            {
                ants[i].ping();
                displayAntennaStatus(ants[i]);
            }
        }

        static void updateDisplay()
        {
            for (int i = 0; i < ants.Count; i++)
            {
                ClearCurrentConsoleLine();
                displayAntennaStatus(ants[i]);
            }
        }

        static void Main(string[] args)
        {

            // MOKATTAM
            NetworkNode a = new NetworkNode("Mokattam Ant. 1", "10.0.10.101");
            ants.Add(a);

            a = new NetworkNode("Mokattam Ant. 2", "10.0.10.102");
            ants.Add(a);

            // M1
            //a = new NetworkNode("M1 Swtich", "192.168.1.160");
            //ants.Add(a);

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
            //a = new NetworkNode("Degla Swtich", "10.0.10.193");
            //ants.Add(a);

            a = new NetworkNode("Degla Ant. 1", "10.0.10.107");
            ants.Add(a);

            // CAIRO
            //a = new NetworkNode("Cairo Swtich", "192.168.1.10");
            //ants.Add(a);

            a = new NetworkNode("Cairo Ant. 1", "192.168.1.109");
            ants.Add(a);

            a = new NetworkNode("Cairo Ant. 2", "10.0.10.105");
            ants.Add(a);

            // M2
            //a = new NetworkNode("M2 Switch", "192.168.1.161");
            //ants.Add(a);

            a = new NetworkNode("M2 Ant. 1", "10.0.10.111");
            ants.Add(a);

            a = new NetworkNode("M2 Ant. 2", "10.0.10.112");
            ants.Add(a);

            a = new NetworkNode("M2 Ant. 3", "10.0.10.114");
            ants.Add(a);

            // TOHAMY
            //a = new NetworkNode("Tohamy Switch", "10.0.10.113");
            //ants.Add(a);

            a = new NetworkNode("Tohamy Ant. 1", "10.0.10.113");
            ants.Add(a);

            // M3
            //a = new NetworkNode("M3 Switch", "192.168.1.162");
            //ants.Add(a);

            a = new NetworkNode("M3 Ant. 1", "10.0.10.115");
            ants.Add(a);

            a = new NetworkNode("M3 Ant. 2", "10.0.10.116");
            ants.Add(a);

            a = new NetworkNode("M3 Ant. 3", "10.0.10.118");
            ants.Add(a);

            // Gendaly 1
            //a = new NetworkNode("Gendaly 1 Switch", "192.168.1.184");
            //ants.Add(a);

            a = new NetworkNode("Gendaly 1 Ant. 1", "10.0.10.117");
            ants.Add(a);

            // Voda 4253
            a = new NetworkNode("Vodafone 4253 1", "10.0.10.119");
            ants.Add(a);

            a = new NetworkNode("Vodafone 4253 2", "10.0.10.120");
            ants.Add(a);

            a = new NetworkNode("Vodafone 4253 3", "10.0.10.140");
            ants.Add(a);

            // Voda 4200
            a = new NetworkNode("Vodafone 4200 1", "10.0.10.137");
            ants.Add(a);

            a = new NetworkNode("Vodafone 4200 2", "10.0.10.141");
            ants.Add(a);

            a = new NetworkNode("Vodafone 4200 3", "10.0.10.142");
            ants.Add(a);

            // Kilo 61
            a = new NetworkNode("Kilo 61", "10.0.10.143");
            ants.Add(a);

            // Kilo 59
            a = new NetworkNode("Kilo 59", "10.0.10.155");
            ants.Add(a);

            // Voda 4254
            a = new NetworkNode("Vodafone 4254 1", "10.0.10.122");
            ants.Add(a);

            a = new NetworkNode("Vodafone 4254 2", "10.0.10.152");
            ants.Add(a);

            //ExcelToNode.getExcelFile();
            //ants = ExcelToNode.getNodesFromExcel();
            //// this method desplays the ips one by one
            //updateNetworkAndDisplay();

            //// this loop is for realtime update of the status of the ips
            while (true)
            {
                Console.SetCursorPosition(0, 0);
                updateNetwork();
                updateDisplay();
                Console.WriteLine("=====");
                Thread.Sleep(timePerPing * 1000);
            }

            Console.ReadKey();
            
        }
    }
}
