using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordChecker.Hashing
{
    internal class handler
    {
        private md5 m_md5;
        private sha1 m_sha1;
        private sha2 m_sha2;
        private sha3 m_sha3;

        private string m_sInput;

        private List<string> vs_hashes = new List<string>();

        public handler()
        {
            m_md5 = new md5();
            m_sha1 = new sha1();
            m_sha2 = new sha2();
            m_sha3 = new sha3();
        }

        public string setUserInput(string userInput)
        {
            m_sInput = userInput;
            //List<string> vs_returnList = new List<string>();

            //if (getHashes()) {
            //   vs_returnList = vs_hashes;
            //}

            Console.WriteLine();
            return m_sha1.getHash(m_sInput);
        }

        private bool getHashes()
        {
            vs_hashes.Add(m_md5.getHash(m_sInput));
            vs_hashes.Add(m_sha1.getHash(m_sInput));
            vs_hashes.Add(m_sha2.getHash(m_sInput));
            if (SHA3_512.IsSupported)
            {
                vs_hashes.Add(m_sha3.getHash(m_sInput));
            }

            return true;
        }
    }
}
