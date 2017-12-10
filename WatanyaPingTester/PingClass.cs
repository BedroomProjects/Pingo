using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace PingAppTest
{
    class PingClass
    {
        private Ping pingObj;
        private PingReply pingReplyObj;
        private String ipAddress;
        private bool reachable = false, pending = true;

        public PingClass()
        {
            pingObj = new Ping();
        }

        public void setIP(String ipAddress)
        {
            this.ipAddress = ipAddress;
        }

        public void ping() {
            pingReplyObj = pingObj.Send(ipAddress);
            if (pingReplyObj.Status == IPStatus.Success) {
                reachable = true;
                pending = false;
            }
            else
            {
                if (pingReplyObj.Status == IPStatus.TimedOut)
                    pending = true;
                else
                    pending = false;

                reachable = false;
            }
        }

        public bool isReacable(){
            return true;
        }

        public bool isPending()
        {
            return pending;
        }

    }
}
