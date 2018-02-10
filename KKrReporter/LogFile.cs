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
       
        public LogFile(string LogFileName)
        {
            try
            {
                file = new System.IO.StreamWriter(LogFileName, true);
            }
            catch (Exception e)
            {
                isEmpty = false;
                //if we are here, it means that will be not able to save anything to the log file. 
                //We can only print exception at the console. Also, we will save all information at variable log backup
                logBackup = logBackup + "Problem with open log file" + "\n";
                logBackup = logBackup + e.GetBaseException().ToString() + "\n";
                logBackup = logBackup + e.StackTrace + "\n";

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
                    logBackup = logBackup + "\n" + firstline;
                }
                file.WriteLine(line);
                logBackup = logBackup + "\n" + line;
                isEmpty = false;
            }
            catch (Exception e)
            {
                isEmpty = false;
                //if we are here, it means that will be not able to save anything to the log file. 
                //We can only print exception at the console. Also, we will save all information at variable log backup
                logBackup = logBackup + "Problem with write to log file" + "\n";
                logBackup = logBackup + e.GetBaseException().ToString() + "\n";
                logBackup = logBackup + e.StackTrace + "\n";

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
                    string line = "End: " + DateTime.Now;
                    file.WriteLine(line);
                    logBackup = logBackup + "\n" + line;
                }
                file.Close();
            }
            catch (Exception e)
            {
                isEmpty = false;
                //if we are here, it means that will be not able to save anything to the log file. 
                //We can only print exception at the console. Also, we will save all information at variable log backup
                logBackup = logBackup + "Problem with close log file" + "\n";
                logBackup = logBackup + e.GetBaseException().ToString() + "\n";
                logBackup = logBackup + e.StackTrace + "\n";
                System.Console.WriteLine(logBackup);

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

        }

    }

