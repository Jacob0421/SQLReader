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

        ExtractGenerator.GenerateExtract("Customers");
    }

}

