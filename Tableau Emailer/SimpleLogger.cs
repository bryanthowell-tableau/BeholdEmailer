using System;
using System.IO;

namespace Behold_Emailer
{
    public class SimpleLogger
    {
        private readonly StreamWriter logfile;

        public SimpleLogger(string logfileLocation)
        {
            this.logfile = File.AppendText(logfileLocation);
            this.Separator();
            this.logfile.AutoFlush = true;
            this.Log("Tableau Emailer started, logging begins");
        }

        public void Log(string logMessage)
        {
            this.logfile.WriteLine("{0} {1} : {2}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString(), logMessage);
        }

        public void Separator()
        {
            this.logfile.WriteLine("-------------------------------");
        }

        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
    }
}