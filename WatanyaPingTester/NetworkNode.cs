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
        private string ipAddress, name, type, road;
        private PingClass pingObj;
        private bool reachable, pending;

        public NetworkNode(string name, string type, string road, string ipAddress)
        {
            this.name = name;
            this.type = type;
            this.road = road;
            this.ipAddress = ipAddress;
            pingObj = new PingClass();
        }

        public string getName()
        {
            return name;
        }

        public string getType()
        {
            return type;
        }

        public string getRoad()
        {
            return road;
        }

        public string getIP()
        {
            return ipAddress;
        }

        public bool isReachable()
        {
            return reachable;
        }

        public string getStatus()
        {
            pingObj.setIP(ipAddress);
            pingObj.ping();
            //if (pingObj.isPending()) return "Pending...";
            if (pingObj.isReachable())
            {
                reachable = true;
                return "Reachable";
            }
            else {
                reachable = false;
                return "Not Reachable"; 
            }
        }
    }
}