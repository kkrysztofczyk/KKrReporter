using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKrReporter
{
    class LogFile
    {
        System.IO.StreamWriter file;
        public bool isEmpty = true;
        public string logBackup;
        private string LogFileName;


        public LogFile(string LogFileName)
        {
            try
            {
                file = new System.IO.StreamWriter(LogFileName, true);
                this.LogFileName = LogFileName;
            }
            catch (Exception e)
            {
                isEmpty = false;
                //if we are here, it means that will be not able to save anything to the log file. 
                //We can only print exception at the console. Also, we will save all information at variable log backup
                logBackup = logBackup + "Problem with open (to write) log file: " + LogFileName + Environment.NewLine;
                logBackup = logBackup + e.GetBaseException().ToString() + Environment.NewLine;
                logBackup = logBackup + e.StackTrace + Environment.NewLine;

                // we will try save infomration to default file 
                Random random = new Random(); 
                try
                {
                    file = new System.IO.StreamWriter("KKrReporterException_" + random.Next(1, 1000).ToString() + "-.log", true);
                    file.WriteLine(DateTime.Now + " " + logBackup);
                    file.Close();
                }
                catch { 
                    //nothin to do in such cases
                }
                // and also we will print that at console
                System.Console.WriteLine(logBackup);
            }
        }

        public void WriteLine(string line)
        {
            try
            {
                if (isEmpty)
                {
                    string firstline = "Start: " + DateTime.Now;
                    file.WriteLine(firstline);
                    logBackup = logBackup + Environment.NewLine + firstline;
                }
                file.WriteLine(line);
                logBackup = logBackup + Environment.NewLine + line;
                isEmpty = false;
            }
            catch (Exception e)
            {
                isEmpty = false;
                //if we are here, it means that will be not able to save anything to the log file. 
                //We can only print exception at the console. Also, we will save all information at variable log backup
                logBackup = logBackup + "Problem with write to log file: " + LogFileName + Environment.NewLine;
                logBackup = logBackup + e.GetBaseException().ToString() + Environment.NewLine;
                logBackup = logBackup + e.StackTrace + Environment.NewLine;

                // we will try save infomration to default file 
                Random random = new Random();
                try
                {
                    file = new System.IO.StreamWriter("KKrReporterException_" + random.Next(1, 1000).ToString() + "-.log", true);
                    file.WriteLine(DateTime.Now + " " + logBackup);
                    file.Close();
                }
                catch
                {
                    //nothin to do in such cases
                }
                // and also we will print that at console
                System.Console.WriteLine(logBackup);
            }
        }

        public void Close()
        {
            try
            {
                if (!isEmpty)
                {
                    string line = "End: " + DateTime.Now + Environment.NewLine;
                    file.WriteLine(line);
                    logBackup = logBackup + Environment.NewLine + line ;
                }
                file.Close();
            }
            catch (Exception e)
            {
                isEmpty = false;
                //if we are here, it means that will be not able to save anything to the log file. 
                //We can only print exception at the console. Also, we will save all information at variable log backup
                logBackup = logBackup + "Problem with close log file: " + LogFileName + Environment.NewLine;
                logBackup = logBackup + e.GetBaseException().ToString() + Environment.NewLine;
                logBackup = logBackup + e.StackTrace + Environment.NewLine;
                System.Console.WriteLine(logBackup);

                // we will try save infomration to default file 
                Random random = new Random();
                try
                {
                    file = new System.IO.StreamWriter("KKrReporterException_" + random.Next(1, 1000).ToString() + "-.log", true);
                    file.WriteLine(DateTime.Now + " " + logBackup + Environment.NewLine);
                    file.Close();
                }
                catch
                {
                    //nothin to do in such cases
                }
                // and also we will print that at console
                System.Console.WriteLine(logBackup);
            }
        }

        }

    }

