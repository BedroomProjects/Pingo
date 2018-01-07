using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatanyaPingTester
{
    class NodePathFromCompany
    {
        List<SchemeNode> schemeNodes = new List<SchemeNode>();
        NodePathInfo nodePathInfo;
        int portIndex;
        List<string> collectingPort;
        // collectingPort List
        //   0                                1
        // Index in scheme as string        PortName
        
        public NodePathFromCompany(List<string> collectingPort, List<SchemeNode> schemeNodes)
        {
            this.collectingPort = collectingPort;
            this.schemeNodes = schemeNodes;
            portIndex = Int32.Parse(collectingPort.ElementAt(0));
            nodePathInfo = new NodePathInfo();
        }

        public void createNodePath()
        {
            List<string> nodeData = new List<string>();
            // Add Port's Info
            nodeData.Add(portIndex.ToString());
            nodeData.Add(schemeNodes[portIndex].getName());
            nodeData.Add(schemeNodes[portIndex].getIP());
            nodePathInfo.nodePathData.Add(nodeData);
            nodePathInfo.portName = schemeNodes[portIndex].getName();

            int prevNodeIndex = schemeNodes[portIndex].getPreviousNodeIndex();
            while(prevNodeIndex != -1){
                nodeData = new List<string>();
                // Add each node's data in port's path
                nodeData.Add(prevNodeIndex.ToString());
                nodeData.Add(schemeNodes[prevNodeIndex].getName());
                nodeData.Add(schemeNodes[prevNodeIndex].getIP());
                nodePathInfo.nodePathData.Add(nodeData);
                // Add index of previous node of current node
                prevNodeIndex = schemeNodes[prevNodeIndex].getPreviousNodeIndex();
                
            }
        }

        public NodePathInfo getNodePathInfo()
        {
            return nodePathInfo;
        }
    }
}
