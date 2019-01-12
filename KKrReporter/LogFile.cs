using System;
using System.IO;

namespace KKrReporter
{
    class LogFile
    {
        System.IO.StreamWriter file;
        public bool isEmpty = true;
        public bool wasError = false;
        public string logBackup;
        private string LogFileName;
        private string LogFilePath;

        public LogFile(string LogFileName)
        {
            try
            {
                //toDo - we are assuming that logFileName is only file name not full paht - that should be checked
                LogFilePath = Path.Combine(Directory.GetCurrentDirectory(), LogFileName);
                file = new System.IO.StreamWriter(LogFileName, true);
                this.LogFileName = LogFileName;
            }
            catch
            {            
                //if we are here, it means that will be not able to save anything to the proper log file. 
                //We can only print exception at the console and try create tmp log file.
                //Also, we will save all information at variable log backup 
                string logMessage = string.Format(Messages.ResourceManager.GetString("E001_NoAccessToLogFile"), LogFilePath);
                logBackup = logBackup + logMessage + Environment.NewLine;
                // and also we will print that at console
                System.Console.WriteLine(logMessage);
                // we will try save infomration to tmp log file
                Random random = new Random();
                try
                {
                    LogFilePath = Path.Combine(Directory.GetCurrentDirectory(), "KKrReporterException_" + random.Next(1, 1000).ToString() + ".log");
                    file = new System.IO.StreamWriter(LogFilePath, true);
                    //To Do Refactoring.LogFile class shouldn't put anything to console 
                    //if something should be displayed at the console it should be returned to program and displayed at the program. 
                    //That could be useful if dll version of the app is planned
                    Console.WriteLine(string.Format(Messages.ResourceManager.GetString("INF02_TempLogFileInfo"), ((FileStream)file.BaseStream).Name));
                    logMessage = logMessage + string.Format(Messages.ResourceManager.GetString("INF02_TempLogFileInfo"), ((FileStream)file.BaseStream).Name);
                    this.WriteLine(logMessage);
                }
                catch
                {
                    //nothin to do in such cases
                }
            }
        }

        public void WriteLine(string message, bool notDisplayOnConsoleIfError = false)
        {
            try
            {
                if (isEmpty)
                {
                    string firstline = string.Format(Messages.ResourceManager.GetString("INF02_AppStartLog"), DateTime.Now);
                    file.WriteLine(firstline);
                    logBackup = logBackup + Environment.NewLine + firstline;
                    isEmpty = false;
                }
                file.WriteLine(message);
                logBackup = logBackup + Environment.NewLine + message;
                isEmpty = false;
            }
            catch
            {
                isEmpty = false;
                //if we are here, it means that will be not able to save anything to the log file. 
                //We can only print exception at the console. Also, we will save all information at variable log backup

                string logMessage = string.Format(Messages.ResourceManager.GetString("E003_NoAccessWriteLogFile"), LogFilePath);
                logBackup = logBackup + logMessage + Environment.NewLine;
                logBackup = logBackup + message + Environment.NewLine;
                System.Console.WriteLine(logMessage);

                // and also we will print that at console, except if was request to not do that. for example because it was already displayed.
                if (notDisplayOnConsoleIfError == false)
                {
                    System.Console.WriteLine(message);
                }

                // we will try save infomration to default file 
                Random random = new Random();
                try
                {
                    file = new System.IO.StreamWriter("KKrReporterException_" + random.Next(1, 1000).ToString() + "-.log", true);
                    file.WriteLine(logBackup);
                    file.Close();
                }
                catch
                {
                    //nothin to do in such cases
                }
            }
        }

        public void Close()
        {

            string lastLine = string.Format(Messages.ResourceManager.GetString("INF03_AppCloseLog"), DateTime.Now) + Environment.NewLine; ;
            this.WriteLine(lastLine);
            logBackup = logBackup + Environment.NewLine + lastLine;

            try
            {
                file.Close();
            }
            catch
            {
                isEmpty = false;
                //if we are here, it means that will be not able to save anything to the log file. 
                //We can only print exception at the console. Also, we will save all information at variable log backup
                string logMessage = string.Format(Messages.ResourceManager.GetString("E004_NoAccessCloseLogFile"), LogFilePath);
                logBackup = logBackup + logMessage + Environment.NewLine;
                System.Console.WriteLine(logMessage);

                // we will try save infomration to default file 
                Random random = new Random();
                try
                {
                    file = new System.IO.StreamWriter("KKrReporterException_" + random.Next(1, 1000).ToString() + "-.log", true);
                    file.WriteLine(logBackup);
                    file.Close();
                }
                catch
                {
                    //nothin to do in such cases
                }
            }
        }
    }
}