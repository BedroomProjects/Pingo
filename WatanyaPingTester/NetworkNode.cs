﻿using System;
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
        private bool reachable = false, pending, timeout = false;

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

        public void abortPingThread()
        {
            pingObj.abortThread();
        }

        public string getStatus()
        {
            if (reachable)
            {
                return "Online";
            }
            else if (timeout)
            {
                return "Timeout";
            }
            else 
            {
                return "Not Reachable";
            }
        }

        public void sendPing()
        {
            pingObj.setIP(ipAddress);
            pingObj.sendPing();

            if (pingObj.isReachable())
            {
                reachable = true;
                timeout = false;
            }
            else
            {
                reachable = false;
                if (pingObj.isTimeOut()) 
                { 
                    timeout = true;
                }
                else
                {
                    timeout = false;
                }
            }
        }
    }
}