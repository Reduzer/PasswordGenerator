using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Drawing;
using System.Globalization;


namespace Program
{
    class generateWithHash
    {
        private string DateTime;
        private string SystemLang;

        private int length;

        CultureInfo ci;

        SHA512 sha;
        

        public generateWithHash()
        {
            ci = CultureInfo.InstalledUICulture;
            
        }

        public string generate(int length)
        {
            string sReturnString = String.Empty;

            this.length = length;

            sReturnString = getHash();

            return sReturnString;
        }

        private string getHash()
        {
            getInfo();

            byte[] hashArr;

            sha = SHA512.Create();
            
            hashArr = sha.ComputeHash(Encoding.UTF8.GetBytes(DateTime + SystemLang));

            hashArr = skrinkToLength(hashArr);

            return Convert.ToBase64String(hashArr);
        }

        private void getInfo()
        {
            DateTime = System.DateTime.Now.ToString();
            SystemLang = ci.Name;

        }

        private byte[] skrinkToLength(byte[] inp)
        {
            byte[] temp = new byte[length];

            for (int i = 0; i < length-1; i++)
            {
                temp[i] = inp[i];
            }

            return temp;
        }
    }
}