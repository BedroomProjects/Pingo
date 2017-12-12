using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.NetworkInformation;

namespace WatanyaPingoConsole
{
    class NetworkNode
    {
        string ip, name, type, road;
        PingReply rp;
        bool reachable = false, pending = true, error = false;

        public NetworkNode(string name, string ipAddress)
        {
            this.ip = ipAddress;
            this.name = name;
        }

        public void ping()
        {
            Ping ping = new Ping();
            try
            {
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
            catch (Exception e)
            {
                pending = false;
                reachable = false;
                error = true;
            }
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
            try
            {
                if (rp.Status == IPStatus.Success)
                {
                    return "Success";
                }
                else
                {
                    if (rp.Status == IPStatus.TimedOut) return "Timeout";
                    else return "Fail";
                }
            }
            catch (Exception e)
            {
                return "Error";
            }
        }
    }
}