using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Hashing
{
	internal interface IHashingAlgorithm
	{
		public byte[] HashData(byte[] abData);
		public bool ValidateHash(byte[] abData, byte[] abHash);
	}
}
