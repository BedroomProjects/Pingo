using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.NetworkInformation;

namespace WatanyaPingTester
{
    class Antenna
    {
        private string ip;
        private string name;
        private bool reachable;
        bool pending = true;

        public Antenna(string n, string i)
        {
            this.ip = i;
            this.name = n;
            //sendPing();
        }

        public void ping()
        {
            Ping ping = new Ping();
            PingReply rp;
            rp = ping.Send(ip);
            if (rp.Status == IPStatus.Success)
            {
                reachable = true;
                pending = false;
            }
            else
                if (rp.Status == IPStatus.TimedOut) pending = true;
                else pending = false;
            reachable = false;
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
            if (reachable) return "Reachable";
            else return "Not Reachable";
        }
    }
}
s