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

        public string ConnectionString { get; private set; }
        public int TimeOut { get; private set; }
        public int DropboxDelay { get; private set; }
        private string timeOutStr;
        private string dropboxDelayStr;
        public string OutDirectory { get; private set; }
        public string AddDate { get; private set; }
        public string BussinessLogFile { get; private set; }
        public string ExceptionLogFile { get; private set; }

        public bool Exists { get; private set; }
        public bool IsReadable { get; private set; }
        public bool IsCorrect { get; private set; }

        private string errorDesctiption;
        private string configFileName;
        private string[] configFileAllLines;
        
        private bool hasWriteAccessToFolder(string folderPath)
        {
            System.IO.StreamWriter file;
            string tempFileName;
            tempFileName = Path.Combine(folderPath, Guid.NewGuid().ToString());
            
            try
            {
                file = new System.IO.StreamWriter(tempFileName, true);
                file.WriteLine(DateTime.Now + " Test File created on second to test if KKrReporter has access to that folder.");
                file.Close();
                return true;
            }
            catch
            {
                return false;
            }
            finally {
                File.Delete(tempFileName);
            }
        }

        private bool hasWriteAccessToFile(string filePaht)
        {
            System.IO.StreamWriter file;
            try
            {
                file = new System.IO.StreamWriter(filePaht, true);
                file.Close();
                return true;
            }

            catch
            {
                return false;
            }
        }

        private bool CheckParameters()
        {
            bool isCorrect = true;

            if (ConnectionString.Contains('='))
            {
                ;
                //we only very roufly checking correctnes of connection string. 
                //We will make sure if it is correct during connection to database.
            }
            else
            {
                errorDesctiption += "connection string: \"" + ConnectionString + "\" is not proper" + Environment.NewLine;
                errorDesctiption += "Issue Code: E005" + Environment.NewLine;
                isCorrect = false;
            }

            if (Directory.Exists(OutDirectory) && hasWriteAccessToFolder(OutDirectory))
            {
            }
            else
            {
                errorDesctiption += "output directory: " + OutDirectory + " is not proper, ";
                errorDesctiption += "patch does not exist or access is not allowed" + Environment.NewLine;
                errorDesctiption += "Issue Code: E006" + Environment.NewLine;
                isCorrect = false;
            }

        
            if (TimeOut >= 0 && TimeOut < 86400)
            {
            }
            else
            {
                errorDesctiption += "TimeOut value: " + TimeOut + " is not proper, ";
                errorDesctiption += "it should be value between 0 and 86400, 0 means that timeout is not changed" + Environment.NewLine;
                errorDesctiption += "(taken from connection string or database configuration)" + Environment.NewLine;
                errorDesctiption += "Issue Code: E007b" + Environment.NewLine;
                isCorrect = false;
            }

            if (DropboxDelay >= 0 && DropboxDelay < 3600)
            {
            }
            else
            {
                errorDesctiption += "timeout value: " + DropboxDelay + " is not proper, ";
                errorDesctiption += "it should be value between 0 and 3600, 0 means that there is no delay" + Environment.NewLine; ;
                errorDesctiption += "Issue Code: E008b" + Environment.NewLine;
                isCorrect = false;
            }

            if (AddDate.ToUpper()=="YES" || AddDate.ToUpper() == "NO"){
                
            }
            else
            {
                errorDesctiption += "AddDate value: " + AddDate + " is not proper, ";
                errorDesctiption += "it should be YES or NO" + Environment.NewLine; ;
                errorDesctiption += "Issue Code: E009" + Environment.NewLine;
                isCorrect = false;
            }

            if (hasWriteAccessToFile(BussinessLogFile) ==true)
            {              
            }
            else
            {
                errorDesctiption += "Business Log File: " + BussinessLogFile + " is not avaible to write, ";
                errorDesctiption += "you have to put file that is available to write for KKrReporter";
                errorDesctiption += Environment.NewLine; ;
                errorDesctiption += "Issue Code: E010a" + Environment.NewLine;
                isCorrect = false;
            }

            if (hasWriteAccessToFile(ExceptionLogFile) == true)
            {
            }
            else
            {
                errorDesctiption += "Exception Log File: " + ExceptionLogFile + " is not avaible to write, ";
                errorDesctiption += "you have to put file that is available to write for KKrReporter";
                errorDesctiption += Environment.NewLine; ;
                errorDesctiption += "Issue Code: E010b" + Environment.NewLine;
                isCorrect = false;
            }
                        
            if (isCorrect)
                return true;
            else
                return false;
        }

        public string GetErrorDesctiption()
        {
            return errorDesctiption;
        }

        private bool Load()
        {
            var dic = configFileAllLines.Select(l => l.Split(new[] { '|' })).ToDictionary(s => s[0].Trim(), s => s[1].Trim());

            if (dic.ContainsKey("ConnectionString"))
            {
                ConnectionString = dic["ConnectionString"];
            }
            else
            {
                errorDesctiption += "Obligatory position \"ConnectionString\" is missing" + Environment.NewLine;
                errorDesctiption += "Issue Code: E004" + Environment.NewLine;
                return false;   //it is obligatory parameter
            }


            if (dic.ContainsKey("OutDirectory"))
            {
                OutDirectory = dic["OutDirectory"];
            }
            else
            {
                OutDirectory += System.AppDomain.CurrentDomain.BaseDirectory;   //default current dir
            }


            if (dic.ContainsKey("TimeOut"))
            {
                timeOutStr = dic["TimeOut"];
            }
            else
            {
                timeOutStr = "0";   //default 0 with mean that timeout is not changed by aplication 
                                    //(ex is set at connection string or at database configuration)
            }

            try
            {
                TimeOut = int.Parse(timeOutStr);
            }
            catch
            {
                errorDesctiption += "TimeOut value: " + timeOutStr + " is not proper, ";
                errorDesctiption += "it should be value between 0 and 86400, 0 means that timeout is not changed" + Environment.NewLine;
                errorDesctiption += "(taken from connection string or database configuration)" + Environment.NewLine;
                errorDesctiption += "Issue Code: E007" + Environment.NewLine;
                return false;
            }


            if (dic.ContainsKey("DropboxDelay"))
            {
                dropboxDelayStr = dic["DropboxDelay"];
            }
            else
            {
                dropboxDelayStr = "10";   //default 10 should be enough for most cases
            }

            try
            {
                DropboxDelay = int.Parse(dropboxDelayStr);
            }
            catch
            {
                errorDesctiption += "timeout value: " + dropboxDelayStr + " is not proper, ";
                errorDesctiption += "it should be value between 0 and 3600, 0 means that there is no delay" + Environment.NewLine; ;
                errorDesctiption += "Issue Code: E008" + Environment.NewLine;
                return false;
            }


            if (dic.ContainsKey("AddDate"))
            {
                AddDate = dic["AddDate"];
            }
            else
            {
                AddDate = "YES";   //default YES
            }


            if (dic.ContainsKey("BussinessLogFile"))
            {
                BussinessLogFile = dic["BussinessLogFile"];
            }
            else
            {
                BussinessLogFile = "KKrReporterBusiness.log";   //KKrReporterBusiness.log
            }


            if (dic.ContainsKey("ExceptionLogFile"))
            {
                ExceptionLogFile = dic["ExceptionLogFile"];
            }
            else
            {
                ExceptionLogFile = "KKrReporterException.log"; //KKrReporterException.log
            }

            return true; //We have all values that should be at config file (not validated yet)
        }


        public ConfigurationFile(string configFile)
        {
            this.configFileName = configFile;
            IsCorrect = true;

            //we are checking if file exist, if confirguration file not exist we set flag and there is nothing to do more
            if (File.Exists(configFileName))
            {
                Exists = true;
            }
            else
            {
                Exists = false;
                IsCorrect = false;
                errorDesctiption += "configurationfile file: " + configFileName + " not exist " + Environment.NewLine;
                errorDesctiption += "Issue Code: E002";
                errorDesctiption += Environment.NewLine;

                return;
            }

            //we are triing to read file, if it is not possible we set flag and there is nothing to do more
            try
            {
                configFileAllLines = File.ReadAllLines(configFileName);
                IsReadable = true;

            }
            catch
            {
                IsReadable = false;
                IsCorrect = false;
                errorDesctiption += "There is problem with opening configuration file: " + configFileName;
                errorDesctiption += ", file exist but it is not possible to read that file." + Environment.NewLine;
                errorDesctiption += "Issue Code: E003" + Environment.NewLine;
                return;
            }

            //we loading file, if false is returned it maans that obligatory parameter was not found (it is connection string)

            if (Load() == false)
            {
                IsCorrect = false;
                return;
            }

            if (CheckParameters() == false)
            {
                IsCorrect = false;
                return;

            }

        }
    }
}