using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.NetworkInformation;

namespace WatanyaPingTester {
    class NetworkNode {
        private string ipAddress, name, nodeType, road, previousNodeIndex, placeType, previousNode;
        private PingClass pingObj;
        private bool reachable = false, timeout = false;

        public NetworkNode(string name, string placeType, string ipAddress, string type, string road, string nextNode, string previousNode) {
            this.name = name;
            this.nodeType = type;
            this.road = road;
            this.ipAddress = ipAddress;
            this.placeType = placeType;
            this.previousNodeIndex = nextNode;
            this.previousNode = previousNode;
            pingObj = new PingClass();
        }

        public string getName() {
            return name;
        }

        public string getNodeType() {
            return nodeType;
        }

        public string getRoad() {
            return road;
        }

        public string getIP() {
            return ipAddress;
        }

        public int getPreviousNodeIndex() {
            return Int32.Parse(previousNodeIndex);
        }

        public string getPlaceType() {
            return placeType;
        }

        public string getPreviousNode() {
            return previousNode;
        }

        public bool isReachable() {
            return reachable;
        }

        public void abortPingThread() {
            pingObj.abortThread();
        }

        public string getStatus() {
            if (reachable) {
                return "Online";
            } else if (timeout) {
                return "Timeout";
            } else {
                return "Not Reachable";
            }
        }

        public void sendPing() {
            pingObj.setIP(ipAddress);
            pingObj.sendPing();

            if (pingObj.isReachable()) {
                reachable = true;
                timeout = false;
            } else {
                reachable = false;
                if (pingObj.isTimeOut()) {
                    timeout = true;
                } else {
                    timeout = false;
                }
            }
        }
    }
}