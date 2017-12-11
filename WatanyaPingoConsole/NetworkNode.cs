using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.NetworkInformation;

namespace WatanyaPingTester
{
    class NetworkNode
    {
        private string ip, name, type, road;
        PingReply rp;
        private bool reachable = false, pending = true;

        public NetworkNode(string name, string ipAddress)
        {
            this.ip = ipAddress;
            this.name = name;
        }

        public void ping()
        {
            Ping ping = new Ping();
            rp = ping.Send(ip);
            if (rp.Status == IPStatus.Success)
            {
                reachable = true;
            }
            else
            {
                
                reachable = false;
            }
            pending = false;
        }

        public void sendPing()
        {
            Thread t = new Thread(ping);
            t.Start();
        }

        public string getName()
        {
            return name;
        }

        public string getIP()
        {
            return ip;
        }

        public string getStatus()
        {
            if (pending) return "Pending...";
            if (rp.Status == IPStatus.Success)
            {
                return "Success";
            }
            else
            {
                if (rp.Status == IPStatus.TimedOut) return "Fail";
                else return "Fail";
            }
        }
    }
}