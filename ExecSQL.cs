using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SQLReader
{
    public class ExecSQL
    {
        public ExecSQL()
        {

        }

        public static dynamic ExecStoredProc(string connectionString, string storedProcName, string? outputFileLocation)
        {
            try
            {
                using (SqlConnection connection = new(connectionString))
                {
                    SqlCommand cmd = new(storedProcName, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    connection.Open();

                    SqlDataReader SQLReader = cmd.ExecuteReader();

                    object[] columns = new object[SQLReader.FieldCount];

                    for (int i = 0; i < SQLReader.FieldCount; i++)
                    {
                        columns[i] = SQLReader.GetName(i);
                    }

                    try
                    {

                        if (outputFileLocation != null)
                        {
                            if (!Path.Exists(outputFileLocation))
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(outputFileLocation));
                            }

                            StreamWriter outputFile = new(outputFileLocation);

                            outputFile.WriteLine(String.Join(",", columns));

                            while (SQLReader.Read())
                            {
                                SQLReader.GetValues(columns);
                                outputFile.WriteLine(String.Join(",", columns));
                            }

                            outputFile.Close();
                        }
                        else
                        {

                            SQLReader.GetValues(columns);
                        }
                    }
                    catch (Exception e)
                    {
                        //ProcessLogger.Log("Could not read output from SQL Server see error:\n" + e.ToString(), "Error");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                //ProcessLogger.Write("Error connecting to Database see error:\n" + e.ToString(), "Error");
            }

            return "";
        }

    }
}
