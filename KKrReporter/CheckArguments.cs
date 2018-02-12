using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKrReporter
{
    class CheckArguments
    {
        string[] args;
        string errorDescription;

        public CheckArguments(string[] args)
        {
            args = this.args;
                    
        }

        public bool isCorrect()
        {
            // correct arguments number is 3 for CSV (if more other are ignored)
            // 6 or 7 for XLSX file

            if (args.Length < 3) {
                errorDescription = "there are 3 obligatory arguments in case of CSV format and 6 obligatory aguments in case of " +
                    "XLSX format, you put: " + args.Length.ToString() + " more about arguments you can read at " +
                    "https://github.com/kkrysztofczyk/KKrReporter/wiki/Application-Arguments" + "\n";
            }



            /* obligatory arguments for CSV format
                        c:\KKrReporter\local.config - configuration file
                        c:\Users\KKr\Dropbox\iAlbatros KKr\SQL\Pierre SQL\_Pierre Report 01.sql - SQL file
                        CSV - format (CSV or XLSX)
                        */

            /* obligatory arguments for XLSX format
            XLSX - format (CSV or XLSX) 
            "XlsxTemplate\Template.xlsx" - XLSX template
            sheetName - sheet name
            1 - column number that data should be put
            2 - row number that data should be put
            */

            /*optional arguments for XLSX format
            0 - master row that contain example of formated values (0 means now master row)
            */



            return true;
        }

        public string GetErrorDesctiption()
        {
            return "";
        }
    }
}
