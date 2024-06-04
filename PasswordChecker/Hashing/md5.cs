using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordChecker.Hashing
{
    internal class md5
    {
        private MD5 m_md5Class;

        public md5()
        {

        }

        public string getHash(string input)
        {
            return calchash(input);
        }

        private string calchash(string sUserInput)
        {
            string returnString = sUserInput;

            using (m_md5Class = MD5.Create())
            {
                byte[] bytes;

                bytes = m_md5Class.ComputeHash(Encoding.UTF8.GetBytes(returnString));

                var sb = new StringBuilder(bytes.Length * 2);

                for (int i = 0; i < 5; i++)
                {
                    byte b = bytes[i];
                    sb.Append(b.ToString("X2"));
                }

                returnString = sb.ToString();
            }

            Debug.WriteLine("Calculated MD5 Hash: " + "\n" + returnString);

            return returnString;
        }
    }
}