using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Configuration;

namespace SQLReader
{
    public class ConfigHelper
    {

        private static XElement configXML;

        public ConfigHelper() {

            Console.WriteLine(ConfigurationManager.AppSettings["PrimaryConfig"]);
            var loadedXML = XElement.Load(ConfigurationManager.AppSettings["PrimaryConfig"]);

            if (loadedXML == null) {
                throw new Exception("Unable to load config file from the Primary Config. Please check that C:\\Users\\Jacob\\Documents\\C# Projects\\SQLReader\\SqlReaderConfig.xml was loaded");
            }

            configXML = loadedXML;
        }

        public string GetConnectionString(string DbName)
        {
            try
            {
                string connStr = configXML.Descendants("Connection").FirstOrDefault(x => x.Attribute("DBName").Value == DbName).Value;
                return connStr;
            } catch(Exception e)
            {
                throw new Exception("No Connection String found in the Primary Config for " + DbName + ".");
            }
        }

        public XElement GetExtractXML(string extractName)
        {
            try
            {
                XElement extractXML = configXML.Descendants("Extract").First(x => (string)x.Attribute("ExtractName") == extractName);
                return extractXML;

            } catch (InvalidOperationException)
            {
                throw new Exception("No extract found in the Primary Config for " + extractName + ".");
            }
        }

    }
}
