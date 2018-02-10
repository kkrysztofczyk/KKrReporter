using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKrReporter
{
    class ConfigurationFile
    {
        public string OutDirectory;
        public string ConnectionString;
        public int TimeOut;
        public string AddDate;
        public string BussinesLogFile;
        public string ExceptionLogFile;

        /* public string emailHost;
         public int emailPort;
         public string emailLogin;
         public string emailPassword;*/

        public string reportDelivery;

        public bool Exists () {
            return true;
        }

        public bool isReadable() {
            return true;
        }

        public bool isCorrect ()
        {
            return true;
        }

        public string GetErrorDesctiption()
        {
            return "";
        }


        public ConfigurationFile(string configFile)
        {

            var dic = File.ReadAllLines(configFile)
              .Select(l => l.Split(new[] { '|' }))
              .ToDictionary(s => s[0].Trim(), s => s[1].Trim());

            OutDirectory = dic["OutDirectory"];
            ConnectionString = dic["ConnectionString"];
            TimeOut = int.Parse(dic["TimeOut"]);
            AddDate = dic["AddDate"];
            BussinesLogFile= dic["BussinesLogFile"];
            ExceptionLogFile = dic["ExceptionLogFile"];


        }
    }
}