using System;
using System.IO;
using System.Xml;

namespace ConsoleFileOperator
{
    public class XmlUtil
    {
        public string GetNodeValueByName(string name)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Environment.CurrentDirectory);
            string xmlName = Path.Combine(dirInfo.Parent.Parent.FullName, "Config.xml");

            XmlReaderSettings setting = new XmlReaderSettings();
            setting.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(xmlName, setting);

            XmlDocument xml = new XmlDocument();
            xml.Load(reader);
            reader.Close();

            XmlNode root = xml.SelectSingleNode("root");
            XmlNodeList list = root.ChildNodes;
            foreach (var item in list)
            {
                XmlElement ele = (XmlElement)item;
                if (ele.Name.Equals(name))
                {
                    if (!string.IsNullOrEmpty(ele.InnerText))
                    {
                        return ele.InnerText.Trim();
                    }
                }
            }
            return null;
        }
    }
}
