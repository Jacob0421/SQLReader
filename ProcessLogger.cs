using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLReader
{
    public class ProcessLogger
    {

        //TODO: Add DB connection and output to logging table of SQL Server as well.
        public ProcessLogger() { 
        
        }

        public static void Log(string message, string messageSeverity, string processID, string processName, string? processor, string? threadNum) {

            LogToConsole(processID, processName, messageSeverity, message, processor, threadNum);

        }

        private static void LogToConsole(string processID, string processName, string severity,string? processor, string? threadNum, string message)
        {
            Console.WriteLine("Error in ProcessId: " + processID + "- " + processName + "\n" +
                "Severity: " + severity + "\n");

            if (processor != null)
            {
                Console.WriteLine("Processor: " + processor);
            }
            if(threadNum != null)
            {
                Console.WriteLine("Thread Number: " + threadNum);
            }

            Console.WriteLine(message);

            return;
        }

        private static void LogToFile()
        {
            return;
        }

        private static void LogToSQL()
        {
            return;
        }

    }
}
