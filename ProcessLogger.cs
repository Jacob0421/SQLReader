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

        public ProcessLogger() { }

        public static void Write(string message, bool stopProcessing) {

            if (stopProcessing)
            {
                Console.Error.WriteLine(message);
                Process.GetCurrentProcess().Kill();
            } else { Console.Error.WriteLine(message); }

        }

    }
}
