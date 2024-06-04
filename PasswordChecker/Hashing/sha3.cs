using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;

namespace PasswordChecker.Hashing
{
    internal class sha3
    {
        public sha3()
        {

        }

        public string getHash(string sUserInput)
        {
            return calcHash(sUserInput);
        }

        private string calcHash(string sUserInput)
        {
            string sReturnString = sUserInput;

            if (SHA3_512.IsSupported)
            {
                byte[] bytes = SHA3_512.HashData(Encoding.UTF8.GetBytes(sReturnString));

                var sb = new StringBuilder();

                for (int i = 0; i < 5; i++)
                {
                    byte b = bytes[i];
                    sb.Append(b.ToString("X2"));
                }

                sReturnString = sb.ToString();
                Debug.WriteLine("Calculated SHA3-512 Hash: " + "\n" + sReturnString);
            }
            else
            {
                sReturnString = "";
                Debug.WriteLine("Sha3 is not Supported on this platform");
            }

            return sReturnString;
        }
    }
}
