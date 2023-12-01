using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SQLReader;

namespace SQLReader
{
    internal class ExtractGenerator
    {

        internal const string configLocation = @"C:\Users\Jacob\Documents\C# Projects\SQLReader\SqlReaderConfig.xml";
        internal string ExtractName = "";
        internal const int fetchXMLRetryCount = 3;

        public ExtractGenerator(string nameOfExtract) { 
            ExtractName = nameOfExtract;
        }


        private static XElement GetExtractElement()
        {

            XElement? configXML = null; 
            XElement? outputElement = null;

            int retrycount = 0;

            bool isFound = false;

            while (!isFound && retrycount < fetchXMLRetryCount)
            {
                try
                {
                    configXML = XElement.Load(configLocation);
                    outputElement = configXML.Elements("Extract").Single(x => x.Attribute("ExtractName").Value == "Customers");

                } catch (Exception e)
                {
                    retrycount++;

                    ProcessLogger.Write("unable to load XML for Customers from config location. starting retry retryCount of fetchXML RetryCount", false);
                    //unable to load XML for Customers from config location. starting retry retryCount of fetchXML RetryCount
                }
            }

            return outputElement;
        }
    }
}
