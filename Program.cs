using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

var configLocation = @"C:\Users\Jacob\Documents\C# Projects\SQLReader\SqlReaderConfig.xml";

XElement configXML = XElement.Load(configLocation);

//Attributes are in the same line as the parent. Elements are the children nodes in the XML
var extractElement = configXML.Elements("Extract").Single(x => x.Attribute("ExtractName").Value == "Customers");

string outputFilePath = extractElement.Element("OutputLocation").Value;
string outputFileName = extractElement.Element("OutputFileName").Value;

string SQLConnStr = "Data Source=DESKTOP-JACOB; Initial Catalog = Dev; Integrated Security = True";

if (!Path.Exists(outputFilePath))
{
    Directory.CreateDirectory(outputFilePath);
}

StreamWriter outputFile = new(outputFilePath + outputFileName);

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
        Console.WriteLine(e.ToString());
    } finally { 
        outputFile.Close(); 
        connection.Close();
    }
}