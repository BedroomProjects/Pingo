using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace WatanyaPingTester {
    public static class XmlHelper {
        public static bool NewLineOnAttributes {
            get;
            set;
        }

        public static void appendOnXml(string filePath) {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode root = doc.DocumentElement;
            

            //Create a new node.
            XmlElement elem = doc.CreateElement("price");
            elem.InnerText = "19.95";
            
            root.AppendChild(elem);

            doc.Save(filePath);
        }

        public static void ToXmlFile2(Object obj, string filePath) {
            XmlDocument doc = new XmlDocument();
            XmlNode root, nodeRecordLevel, recordLevel = null;
            XmlElement elem;
            root = doc.CreateElement(obj.GetType().ToString());
            //doc.AppendChild(elem);
            //root = doc.DocumentElement;
            AllNodes allNodes = (AllNodes)obj;
            for (int i = 0; i < allNodes.nodeRecord.Count(); i++) {
                // add nodeRecord Tag
                nodeRecordLevel = doc.CreateElement("nodeRecord");
                // add name Tag
                elem = doc.CreateElement("name");
                elem.InnerText = allNodes.nodeRecord[i].name;
                nodeRecordLevel.AppendChild(elem);
                
                //add record tag
                
                for (int j = 0; j < allNodes.nodeRecord[i].record.Count(); j++) {
                    recordLevel = doc.CreateElement("record");

                    elem = doc.CreateElement("status");
                    elem.InnerText = allNodes.nodeRecord[i].record[j].status;
                    recordLevel.AppendChild(elem);

                    elem = doc.CreateElement("timeDate");
                    elem.InnerText = allNodes.nodeRecord[i].record[j].timeDate;
                    recordLevel.AppendChild(elem);
                    nodeRecordLevel.AppendChild(recordLevel);
                }
                root.AppendChild(nodeRecordLevel);
            }
            doc.AppendChild(root);
            doc.Save(filePath);
        }

        public static List<NodeRecord> readFromXml(string filePath) {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            List<NodeRecord> allNodesRecords = new List<NodeRecord>();
            Record tempRecord = new Record();
            List<Record> recordsList = new List<Record>();
            NodeRecord nodeRecord;

            string nodeName, nodeStatus, timeDate;
            XmlNodeList nodeRecordsList = xmlDoc.SelectNodes("/WatanyaPingTester.AllNodes/nodeRecord");
            foreach (XmlNode xn in nodeRecordsList) {
                nodeRecord = new NodeRecord();
                recordsList = new List<Record>();
                nodeName = xn["name"].InnerText;
                nodeRecord.name = nodeName;

                for (int i = 1; i < xn.ChildNodes.Count; i++) {
                    tempRecord = new Record();
                    nodeStatus = xn.ChildNodes.Item(i).ChildNodes.Item(0).InnerText;
                    timeDate = xn.ChildNodes.Item(i).ChildNodes.Item(1).InnerText;

                    tempRecord.status = nodeStatus;
                    tempRecord.timeDate = timeDate;

                    recordsList.Add(tempRecord);
                }
                
                nodeRecord.record = recordsList;
                allNodesRecords.Add(nodeRecord);
            }
            //new Reports(filePath).createDDetailsReport(allNodesRecords);
            return allNodesRecords;
        }
    }

    public class AllNodes {
        public List<NodeRecord> nodeRecord = new List<NodeRecord>();
    }
    
    public class NodeRecord {
        public string name;
        public List<Record> record;
    }

    public class Record {
        public string status;
        public string timeDate;
    }
}
