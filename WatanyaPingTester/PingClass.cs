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

        public PingClass()
        {
            pingObj = new Ping();
        }

        public void setIP(String ipAddress)
        {
            this.ipAddress = ipAddress;
        }

        public bool getResponse()
        {
            pingReplyObj = pingObj.Send(ipAddress);
            if (pingReplyObj.Status == IPStatus.Success)
                return true;
            else
                return false;
        }
    }
}
