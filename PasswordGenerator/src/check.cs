using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PasswordChecker;

namespace PasswordGenerator.src
{
    internal class check
    {
        public bool checkPW(string input)
        {
            if (PasswordChecker.PasswordChecker.getInput(input))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
