using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;

namespace PasswordChecker.Hashing
{
    internal class sha2
    {
        private SHA256 m_sha2Class;

        public sha2()
        {

        }

        public string getHash(string sUserInput)
        {
            return calcHash(sUserInput);
        }

        private string calcHash(string sUserInput)
        {
            string sReturnString = sUserInput;

            using (m_sha2Class = SHA256.Create())
            {
                byte[] bytes = m_sha2Class.ComputeHash(Encoding.UTF8.GetBytes(sUserInput));

                var sb = new StringBuilder();

                for (int i = 0; i < 5; i++)
                {
                    byte b = bytes[i];
                    sb.Append(b.ToString("X2"));
                }

                sReturnString = sb.ToString();
                Debug.WriteLine("Calculated SHA2-256 Hash: " + "\n" + sReturnString);
            }

            return sReturnString;

        }
    }
}
