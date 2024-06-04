using System;

using PasswordChecker.Hashing;
using PasswordChecker.CheckDB;

namespace PasswordChecker
{
    public static class PasswordChecker
    {
        private static handler m_hashHandler = new handler();
        private static string hashes;
        private static httpclient m_httpClient = new httpclient();

        public static string response;
        private static string sUserInput;

        public static void Main()
        {

        }

        public static bool getInput(string input)
        {
            sUserInput = input;

            hashes = m_hashHandler.setUserInput(sUserInput);

            httpclient.request(hashes);

            if (response == null)
            {
                return true;
            }

            return false;
        }
    }
}