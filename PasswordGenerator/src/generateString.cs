using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using System.Diagnostics;

namespace PasswordGenerator.Generator
{
    internal class generateString
    {
        Random m_Rng = new Random();

        private readonly char[] charsLowerCase =  {'a','b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'n', 'm', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};
        private readonly char[] charsUpperCase = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private readonly char[] charsOtherChars = {'!', '§', '$', '%', '&', '/', '(', ')', '=', '?', '{', '[', ']', '}', '<', '>', '|', ',', '.', '-', ';', ':', '_', '+', '*', '~', '#', '^', '°'};
        private readonly char[] charNumbers = {'1', '2', '3', '4', '5', '6', '7', '8', '9', '0'};    
    
        public generateString()
        {

        }

        public string generateRandomString(int length)
        {
            string sReturnString = "";

            for (int i = 0; i < length; i++)
            {
                int temp = m_Rng.Next(0,3);

                switch (temp)
                {
                    case 0:
                        temp = m_Rng.Next(0, charsLowerCase.Length);
                        sReturnString += charsLowerCase[temp];
                        break;
                    case 1:
                        temp = m_Rng.Next(0, charsUpperCase.Length);
                        sReturnString += charsUpperCase[temp];
                        break;
                    case 2:
                        temp = m_Rng.Next(0, charsOtherChars.Length);
                        sReturnString += charsOtherChars[temp];
                        break;
                    case 3:
                        temp = m_Rng.Next(0, charNumbers.Length);
                        sReturnString += charNumbers[temp];
                        break;
                    default:
                        break;
                }
            }

            Debug.WriteLine(sReturnString); 

            return sReturnString;
        }
    }
}
