using System.Data.SqlClient;
using System;
using System.IO;
using System.Resources;
using System.Reflection;

namespace KKrReporter
{
    class Program
    {

        static void Main(string[] args)
        {
            //ToDo - wyglądzić komentarze i kod, probrać komunikaty z pliku zasobów
            //https://stackoverflow.com/questions/530539/what-does-0-mean-when-found-in-a-string-in-c
            //https://www.youtube.com/watch?v=fFC8dxtPfp4
            //https://stackoverflow.com/questions/1508570/read-string-from-resx-file-in-c-sharp
            //String.format       
            //Console.WriteLine (rm.GetString("E001_NoArguments"));
                     
            if (args.Length ==0)
            {
                String mainMessage= Messages.ResourceManager.GetString("E001_NoArguments");
                LogFile businessLog = new LogFile("KKrReporter.log"); //temporary log, only to save that information
                businessLog.WriteLine(mainMessage);
                Console.WriteLine(mainMessage);
                Console.WriteLine(Messages.ResourceManager.GetString("INF01_AppClose"));
                System.Threading.Thread.Sleep(25000);
                businessLog.Close();
                return;
            }

            //the most important is configuration file, 
            //we are starting from checking if file is correct
            ConfigurationFile configurationfile = new ConfigurationFile(args[0]);
            if (configurationfile.IsCorrect == false)
            {
                LogFile bussinesLogTmp = new LogFile("KKrReporter.log"); //temporary log, only to save that information
                string message = "Configuration file " + args[0] + " is not valid, " +
                                 "KKrReporter is not able process request without proper configuration file." +
                                  Environment.NewLine +
                                 "Error description: " + configurationfile.GetErrorDesctiption() +
                                 "more about configuration file you can read at " +
                                 "https://github.com/kkrysztofczyk/KKrReporter/wiki/Config-File" +
                                  Environment.NewLine;
                bussinesLogTmp.WriteLine(message);
                bussinesLogTmp.Close();
                System.Console.WriteLine(message);
                System.Threading.Thread.Sleep(25000);
                return;
            }

            //we have at lest correct configuration file, we can start using configuration data
            LogFile bussinesLog = new LogFile(configurationfile.BussinessLogFile);
            //LogFile ExceptionLogFile = new LogFile(configurationfile.ExceptionLogFile);

            //now we can check rest of arguments (configuration file is already checked and it is valid)
            CheckArguments checkarguments = new CheckArguments(args);
            if (checkarguments.IsCorrect() == false)
            {
                string message = "Configuration file " + args[0] + " is not valid, " +
                                 "KKrReporter is not able process request without correct arguments." +
                                  Environment.NewLine +
                                 "Error description: " + checkarguments.GetErrorDesctiption() +
                                 "more about arguments you can read at " +
                                 "https://github.com/kkrysztofczyk/KKrReporter/wiki/Config-File" +
                                  Environment.NewLine;
                bussinesLog.WriteLine(message);
                bussinesLog.Close();
                System.Threading.Thread.Sleep(25000);
                return;
            }


            //we know that there are proper input arguments, now we can process report
            
            string fileOut;
            string connectionString = "";
            string sqlQuery = "";



            //wczytywanie parametrów konfiguracyjnych - do refaktoringu ja bym widział to zamknięte w obiekcie configurationfile


            if (configurationfile.AddDate == "YES")
            {
                fileOut = Path.Combine(configurationfile.OutDirectory, Path.GetFileNameWithoutExtension(args[1]) + " " + DateTime.Now.ToString("yyyy-MM-dd"));
            }
            else
            {
                fileOut = Path.Combine(configurationfile.OutDirectory, Path.GetFileNameWithoutExtension(args[1]));
            }


            //we know that we have correct input data, now we can cretate connection to DB

            connectionString = configurationfile.ConnectionString;
            sqlQuery = File.ReadAllText(args[1]);
            SqlConnection connection = new SqlConnection();
            connection = new SqlConnection();

            /*połaczenie z serwerem, wykonanie zapytania, pobranie wyników i ich zapis do pliku
            try
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                if (args[2] == "CSV")
                {
                    SQLServerHelper.DumpQueryToFileCSV(connection, sqlQuery, configurationfile.TimeOut, fileOut);
                }

                if (args[2] == "XLSX")
                {
                    SQLServerHelper.DumpQueryToFileXLSX(connection, sqlQuery, configurationfile.TimeOut,
                                                        fileOut, args[3], args[4], int.Parse(args[5]), int.Parse(args[6]), int.Parse(args[7]));

                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Problem z połączeniem z serwerem lub zapisem danych do pliku");
                Console.WriteLine(e.GetBaseException());
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                connection.Close();
            }

            Console.WriteLine(DateTime.Now + "; " + args[0] + "; " + args[1]);
            */
            // 
            System.Threading.Thread.Sleep(10000);
        }
    }
}

