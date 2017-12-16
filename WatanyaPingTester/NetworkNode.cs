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
        private string ipAddress, name, nodeType, road, nextNode, placeType;
        private PingClass pingObj;
        private bool reachable, pending;

        public NetworkNode(string name, string placeType, string ipAddress, string type, string road, string nextNode)
        {
            this.name = name;
            this.nodeType = type;
            this.road = road;
            this.ipAddress = ipAddress;
            this.placeType = placeType;
            this.nextNode = nextNode;
            pingObj = new PingClass();
        }

        public string getName()
        {
            return name;
        }

        public string getNodeType()
        {
            return nodeType;
        }

        public string getRoad()
        {
            return road;
        }

        public string getIP()
        {
            return ipAddress;
        }

        public string getNextNode()
        {
            return nextNode;
        }

        public string getPlaceType()
        {
            return placeType;
        }

        public bool isReachable()
        {
            return reachable;
        }

        public string getStatus()
        {
            pingObj.setIP(ipAddress);
            pingObj.sendPing();
            //if (pingObj.isPending()) return "Pending...";
            if (pingObj.isReachable())
            {
                reachable = true;
                return "Online";
            }
            else
            {
                reachable = false;
                return "Not Reachable";
            }
        }
    }
}