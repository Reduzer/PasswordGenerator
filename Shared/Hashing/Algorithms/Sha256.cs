using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Hashing
{
	internal class Sha256 : IHashingAlgorithm
	{
		public byte[] HashData(byte[] abData)
		{
			return SHA256.HashData(abData);
		}

		public bool ValidateHash(byte[] abData, byte[] abHash)
		{
			bool bResult = false;

			if (abHash == SHA256.HashData(abData)) {
				bResult = true;
			}

			return bResult;
		}
	}
}
