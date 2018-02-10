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

        public CheckArguments(string[] args) {
            args = this.args;
        }

        public bool isCorrect()
        {
            return true;
        }

        public string GetErrorDesctiption()
        {
            return "";
        }
    }
}
