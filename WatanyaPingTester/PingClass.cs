using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.NetworkInformation;

namespace WatanyaPingTester
{
    class PingClass
    {
        private Thread t;
        private Ping pingObj;
        private PingReply pingReplyObj;
        private String ipAddress;
        private bool reachable = false, pending = true, error = false, timeOut = false;

        public PingClass()
        {
            pingObj = new Ping();
        }

        public void setIP(String ipAddress)
        {
            this.ipAddress = ipAddress;
        }

        public void ping() {
            try
            {
                pingReplyObj = pingObj.Send(ipAddress);
                if (pingReplyObj.Status == IPStatus.Success)
                {
                    this.reachable = true;
                    pending = false;
                    timeOut = false;
                }
                else
                {
                    if (pingReplyObj.Status == IPStatus.TimedOut)
                    {
                        pending = true;
                        timeOut = true;
                    }
                    else
                    {
                        pending = false;
                        timeOut = false;
                    }

                    this.reachable = false;
                }
            }
            catch (Exception e)
            {
                pending = false;
                reachable = false;
                error = true;
            }
            finally
            {
                t.Abort();
            }
        }

        public void abortThread()
        {
            t.Abort();
        }

        public void sendPing()
        {
            t = new Thread(ping);
            t.Start();
        }

        public bool isReachable()
        {
            return reachable;
        }

        public bool isPending()
        {
            return pending;
        }

        public bool isTimeOut()
        {
            return timeOut;
        }
    }
}
