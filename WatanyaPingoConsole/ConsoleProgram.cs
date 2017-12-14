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
        public static string sokhnaFileName = @"sokhna_scheme.xlsx";
        public static string alexFileName = @"alex_scheme.xlsx";
        public static int consoleChoice = 1;

        static void displayAntennaStatus(NetworkNode a)
        {
            Console.Write("(" + a.getIP() + "): " + a.getName());
            Console.SetCursorPosition(45, Console.CursorTop);
            if (a.getStatus() == "Online")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(a.getStatus());
            }
            else if (a.getStatus() == "Offline")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(a.getStatus());
            }
            else if (a.getStatus() == "Timeout")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(a.getStatus());
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(a.getStatus());
            }
            Console.ResetColor();
        }

        public static void displayMenu()
        {
            Console.SetCursorPosition(0, 2);
            if (consoleChoice == 1)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("Sokhna Road Network");
                Console.ResetColor();
                Console.WriteLine("Alexandria Road Network");
            }
            else
            {
                Console.WriteLine("Sokhna Road Network");
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("Alexandria Road Network");
                Console.ResetColor();
            }
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

        static List<NetworkNode> convertStringsToNodes(List<List<string>> s)
        {
            List<NetworkNode> nn = new List<NetworkNode>();
            for (int i = 0; i < s.Count; i++)
            {
                NetworkNode n = new NetworkNode(s[i][0], s[i][1]);
                nn.Add(n);
            }
            return nn;
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize((Console.LargestWindowWidth)/3, Console.LargestWindowHeight);
            Console.SetWindowPosition(Console.WindowLeft, Console.WindowTop);
            consoleChoice = 1;
            Console.WriteLine("Choose the Network you want to ping:\n");
            displayMenu();
            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();
                if ((keyinfo.Key == ConsoleKey.DownArrow) || (keyinfo.Key == ConsoleKey.UpArrow))
                {
                    if (consoleChoice == 2) consoleChoice = 1; else consoleChoice++;
                    displayMenu();
                }
            }
            while (keyinfo.Key != ConsoleKey.Enter);

            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Preparing Data...");
            string fileName = sokhnaFileName;
            if (consoleChoice == 1)
            {
                fileName = sokhnaFileName;
            }
            else
            {
                fileName = alexFileName;
            }

            ExcelToNode.getExcelFile(fileName);
            ants = convertStringsToNodes(ExcelToNode.getResult());

            Console.SetCursorPosition(0, 0);
            Console.WriteLine(fileName);
            Console.WriteLine("=====");

            //// this method desplays the IPs one by one
            //updateNetworkAndDisplay();

            //// this loop is for realtime update of the status of the IPs
            while (true)
            {
                Console.SetCursorPosition(0, 2);
                updateNetwork();
                updateDisplay();
                Console.WriteLine("=====");
                Thread.Sleep(timePerPing * 1000);
            }

            Console.ReadKey();
            
        }
    }
}
