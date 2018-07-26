using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKrReporter
{
    class CheckArguments
    {
        private string[] args;
        private bool isCorrect;
        private string errorDescription;

        public CheckArguments(string[] args)
        {
            args = this.args;

            /* obligatory arguments for CSV format
            arg[0]: configuration file example: c:\KKrReporter\KKrReporter-LocalMSSQL.config
            arg[1]: SQL file example            c:\KKrReporter\Report_01.sql
            arg[2]: format of report file: CSV or XLSX (for CSV of course should be CSV)
            */

            /* obligatory arguments for XLSX format
            arg[2]: format of report file: CSV or XLSX (for XLSX of course should be XLSX)
            arg[3]: XLSX template used for report, example: c:\KKrReporter\Report_01.xlsx
            arg[4]: XLSX template sheet name that data should be put: example sheet1
            arg[5]:1 - column number that data should be put, example: 1
            arg[6]:2 - row number that data should be put, example: 2
            */

            /*optional arguments for XLSX format (not supported for that moment)
            arg[7]: master row that contain example of formated values (0 means no master row), example: 0 or 2
            */


        }

        public string GetErrorDesctiption()
        {
            return errorDescription;
        }

        private bool IsNumberofArgumentsValid()
        {
            //we are checking if number of argumetnts is correct
            // correct arguments number is 3 for CSV (if more other are ignored)
            // 6 or 7 for XLSX file
            if (args.Length < 3)
            {
                errorDescription = "there are 3 obligatory arguments in case of CSV format and 6 obligatory aguments in case of " +
                    "XLSX format, you put: " + args.Length.ToString() +
                    " more about arguments you can read at " +
                    "https://github.com/kkrysztofczyk/KKrReporter/wiki/Application-Arguments " +
                    "Issue Code: E010, program is not procesiing futher operation " + "\n";
                return false;
            }
            if (args.Length > 7)
            {
                errorDescription = "maximum namber of paramters is 7, you put: " + args.Length.ToString() +
                    " more about arguments you can read at " +
                    "https://github.com/kkrysztofczyk/KKrReporter/wiki/Application-Arguments " +
                    "Issue Code: E011, program is not procesiing futher operation " + "\n";
                return false;
            }
            if (args[2].ToUpper() == "XLSX" && args.Length != 7)
            {
                errorDescription = "there are 7 obligatory arguments in case of XLSX format" +
                    ", you put: " + args.Length.ToString() +
                    " more about arguments you can read at " +
                    "https://github.com/kkrysztofczyk/KKrReporter/wiki/Application-Arguments " +
                    "Issue Code: E013, program is not procesing futher operation " + "\n";
                return false;
            }
            //we was not able to find mistake
            return true;
        }
        
        private bool IsSQLFilleCorrect()
        {

            //at first we are checking if file is existing and extention is .sql
            if (args[1].Substring(args[1].Length - 4).ToLower().Equals(".sql"))
            {
                var kkr = 'a';
            }

                    

            //we can read that file

                //kodowanie UTF-8 lub ASCII 32−126

                //Rozmiar poniżej (na razie przyjmijmy 10 MB)

                //Rozmiar powyżej zera

                //skłądnia wyrażeniem regularnym

            return true;

        }

        private bool IsXLSXFileAndParametersCorrect()
        {
            //at first we are checking if file is existing and extention is .xlsx

            //we can read that file

            //sheet that we would like export data exist

            //row number that we would like place data is not biger than 1 048 576 

            //column number that we would like place data is not biger than 16 384 
            return true;
        }

        public bool IsCorrect()
        {
            if (IsNumberofArgumentsValid() == false)
            {
                return false;
            }

            //we know that first argument (configuration file) is already checked so we can start form second one
            if (this.IsSQLFilleCorrect() == false)
            {
                //toDo ErrorDescription
                errorDescription += "incorrect SQL file";
                return false;
            }

            //we checking if format is choosen correctly (currenty app support CVS and XLSX)
            //if format is CSV we can return true (next validation are only for XLSX)
            if (args[2].ToUpper()!="CSV" && args[2].ToUpper() != "XLSX" )
            {
                //toDo ErrorDescription
                errorDescription += "incorrect output format (allowed values CSV or XLSX)";
                return false;
            }
            else
            {
                if (args[2].ToUpper() != "CSV")
                return true;
                //we have all needed arguments
            }

            if (this.IsXLSXFileAndParametersCorrect() == false)
            {
                //toDo ErrorDescription
                errorDescription += "incorrect XLSX file";
                return false;
            }

            //we was not able to find any error at aplication arguments
            return true;
        }
        

        //todo We have to check if arguments values are correct, it we don't do that it is still not disaster 
        //during program execution, if the argument is wrong it will be cached at the exception, we all exceptions 
        //are handled correctly :)


    }
}
