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
        /// <summary>
        /// Serializes an object to an XML string, using the specified namespaces.
        /// </summary>
        public static string ToXml(object obj, XmlSerializerNamespaces ns) {
            Type T = obj.GetType();

            var xs = new XmlSerializer(T);
            var ws = new XmlWriterSettings {
                Indent = true, NewLineOnAttributes = NewLineOnAttributes, OmitXmlDeclaration = true
            };

            var sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb, ws)) {
                xs.Serialize(writer, obj, ns);
            }
            return sb.ToString();
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

        /// <summary>
        ///   an object to an XML string.
        /// </summary>
        public static string ToXml(object obj) {
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            return ToXml(obj, ns);
        }

        /// <summary>
        /// Deserializes an object from an XML string.
        /// </summary>
        public static T FromXml<T>(string xml) {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StringReader sr = new StringReader(xml)) {
                return (T)xs.Deserialize(sr);
            }
        }

        /// <summary>
        /// Deserializes an object from an XML string, using the specified type name.
        /// </summary>
        public static object FromXml(string xml, string typeName) {
            Type T = Type.GetType(typeName);
            XmlSerializer xs = new XmlSerializer(T);
            using (StringReader sr = new StringReader(xml)) {
                return xs.Deserialize(sr);
            }
        }

        /// <summary>
        /// Serializes an object to an XML file.
        /// </summary>
        public static void ToXmlFile(Object obj, string filePath) {
            var xs = new XmlSerializer(obj.GetType());
            var ns = new XmlSerializerNamespaces();
            var ws = new XmlWriterSettings {
                Indent = true, NewLineOnAttributes = NewLineOnAttributes, OmitXmlDeclaration = true
            };
            ns.Add("", "");

            using (XmlWriter writer = XmlWriter.Create(filePath, ws)) {
                xs.Serialize(writer, obj);
            }
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

        /// <summary>
        /// Deserializes an object from an XML file.
        /// </summary>
        public static T FromXmlFile<T>(string filePath) {
            StreamReader sr = new StreamReader(filePath);
            try {
                var result = FromXml<T>(sr.ReadToEnd());
                return result;
            } catch (Exception e) {
                throw new Exception("There was an error attempting to read the file " + filePath + "\n\n" + e.InnerException.Message);
            } finally {
                sr.Close();
            }
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
