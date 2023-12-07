using SQLReader;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Configuration;

public class Program
{

    static void Main(string[] args)
    {

        Dictionary<string, string> paramDict = new();

        if (args.Length % 2 != 0)
        {
            throw new Exception("Invalid parameters entered.");
        }
        
        for (int i = 0; i < args.Length; i += 2)
        {
            paramDict.Add(args[i].ToLower(), args[i + 1].ToLower());
        }

        var taskType = paramDict["-tt"] ?? paramDict["-tasktype"];
        var taskName  = paramDict["-n"] ?? paramDict["-name"] ?? paramDict["-taskname"];

        if(taskType == "extract") {
            ExtractGenerator.GenerateExtract(taskName);
        }
    }

}

