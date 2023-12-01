using SQLReader;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Xml.Linq;


const string configLocation = @"C:\Users\Jacob\Documents\C# Projects\SQLReader\SqlReaderConfig.xml";

//Attributes are in the same line as the parent. Elements are the children nodes in the XML
var extractElement = GetConfigElement(configLocation);

string outputFilePath = extractElement.Element("OutputLocation").Value;
string outputFileName = extractElement.Element("OutputFileName").Value;

string SQLConnStr = GetConnStr(configLocation);

if (!Path.Exists(outputFilePath))
{
    Directory.CreateDirectory(outputFilePath);
}

StreamWriter outputFile = new(outputFilePath + outputFileName);
try {
    using (SqlConnection connection = new(SQLConnStr))
    {
        SqlCommand cmd = new("GetCustomers", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        connection.Open();

        SqlDataReader SQLReader = cmd.ExecuteReader();

        object[] columns = new object[SQLReader.FieldCount];

        for(int i = 0; i < SQLReader.FieldCount; i++){
            columns[i] = SQLReader.GetName(i);
        }

        outputFile.WriteLine(String.Join(",", columns));

        try
        {
            while (SQLReader.Read())
            {
                SQLReader.GetValues(columns);
                outputFile.WriteLine(String.Join(",", columns));
            }
        } catch (Exception e) {
            ProcessLogger.Write("Could not read output from SQL Server see error:\n" + e.ToString(), true);
        } finally { 
            outputFile.Close(); 
            connection.Close();
        }
    }
}
catch (Exception e)
{
    ProcessLogger.Write("Error connecting to Database see error:\n" + e.ToString(), true);
}



static XElement GetConfigElement(string configLocation)
{
    XElement configXML = XElement.Load(configLocation);

    return configXML.Elements("Extracts").Elements("Extract").Single(x => x.Attribute("ExtractName").Value == "Customers");
}

static string GetConnStr(string configLocation)
{

    XElement configXML = XElement.Load(configLocation);

    return configXML.Elements("DBConnections").Elements("Connection").Single(x => x.Attribute("DBName").Value == "Dev").Element("ConnectionString").Value; ;
}