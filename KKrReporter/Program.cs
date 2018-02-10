using System.Data;
using System.Data.SqlClient;
using System;
using System.IO;
using System.Text;



namespace KKrReporter
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //there are 3 obligatory arguments and 3 optional
            
            ConfigurationFile configurationfile = null;
                        
            configurationfile = new ConfigurationFile(args[0]);

            if (configurationfile.Exists()==false){
                LogFile businessLog = new LogFile("CheckWebbusiness.log");
                businessLog.WriteLine("configurationfile file: " +args[0] + 
                                      " notexist, KKrReporter is not able process request without proper configuration file" + "\n" +
                                      "Issue Code: E002 plesase go to: " + "https://github.com/kkrysztofczyk/KKrReporter/wiki/Errors");
                businessLog.Close();
                return;
            }

            if (configurationfile.isReadable() == false)
            {
                LogFile businessLog = new LogFile("CheckWebbusiness.log");
                businessLog.WriteLine("there is problem with opening configuration file: " + args[0] +
                                      "file exist but it is not possible to read that file, " +
                                      "KKrReporter is not able process request without proper configuration file" + "\n" +
                                      "Issue Code: E003 plesase go to: " + "https://github.com/kkrysztofczyk/KKrReporter/wiki/Errors");
                businessLog.Close();
                return;
            }

            if (configurationfile.isCorrect() == false)
            {
                LogFile businessLog = new LogFile("CheckWebbusiness.log");
                businessLog.WriteLine("Configration file " + args[0] + " is not valid, "+
                                      "KKrReporter is not able process request without proper configuration file" + "\n" +
                                      "Issue Code: E004 plesase go to: " + "https://github.com/kkrysztofczyk/KKrReporter/wiki/Errors" +
                                      "error description:" + "\n" +
                                      configurationfile.GetErrorDesctiption());
                businessLog.Close();
                return;
            }
                 



            string fileOut = "";
            string connectionString = "";
            string sqlQuery = "";

            Console.Write(DateTime.Now + "; "); // to najfajniej jakby także trafiło do loga, 
                                                //to czy na konsoli takze aplikacja wypisuje komunikaty mogło by być w pliku konfiguracyjnym

            ConfigurationFile configurationfile = null;

            //wczytywanie parametrów konfiguracyjnych - do refaktoringu ja bym widział to zamknięte w obiekcie configurationfile
            try
            {
                configurationfile = new ConfigurationFile(args[0]);

                if (configurationfile.AddDate == "YES")
                {
                    fileOut = Path.Combine(configurationfile.OutDirectory, Path.GetFileNameWithoutExtension(args[1]) + " " + DateTime.Now.ToString("yyyy-MM-dd"));
                }
                else
                {
                    fileOut = Path.Combine(configurationfile.OutDirectory, Path.GetFileNameWithoutExtension(args[1]));
                }

                connectionString = configurationfile.ConnectionString;
                sqlQuery = File.ReadAllText(args[1]);
            }
            catch (Exception e)
            {
                Console.WriteLine(DateTime.Now + "; " + args[0] + "; " + args[1]);
                Console.WriteLine("Problem z parametrami wejściowymi");
                Console.WriteLine(e.GetBaseException());
                Console.WriteLine(e.StackTrace);
            }

            SqlConnection connection = new SqlConnection();
            connection = new SqlConnection();

            //połaczenie z serwerem, wykonanie zapytania, pobranie wyników i ich zapis do pliku
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
            // Console.ReadLine();
        }
    }
}

