using System.Xml.Linq;

namespace SQLReader
{
    internal class ExtractGenerator
    {

        public static bool GenerateExtract(string extractName)
        { 
            ConfigHelper configHelper = new ConfigHelper();

            XElement config = configHelper.GetExtractXML(extractName);

            string outputDirectory = config.Element("OutputLocation").Value;
            string outputFilename = config.Element("OutputFileName").Value;
            string sp = config.Element("StoredProcName").Value;
            string DBName = config.Element("DBConnection").Value;

            string connStr = configHelper.GetConnectionString(DBName);

            if(outputDirectory.Last().ToString() != "\\")
            {
                outputDirectory += "\\";
            }

            string outputLocation = outputDirectory + outputFilename;

            ExecSQL.ExecStoredProc(connStr, sp, outputLocation);

            return true; 
        }

    }
}
