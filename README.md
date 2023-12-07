# SQLReader
Mini project learning how to use C# to read from XML config and call a Stored proc from a local instance of Microsoft SQL Server to write out to a CSV 

Project is meant to learn how to take config inputs and create a connection to SQL DB Instance (local), then run a StoredProc on the `Dev.dbo.Customers` table to form an 'extract' and output this data to a .csv file (See ~\TestOutputPath\SQLReaderOutput.csv)

Project currently references a project app config named ~\bin\Debug\net7.0\SQLReader.dll.config which has a reference back to the original SQLReaderConfig.xml.

**NOTE: All data referenced in the system was automatically generated through Mockaroo.com**
