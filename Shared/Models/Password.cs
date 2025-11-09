using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
	public record Password
	{
		public string sData { get; private set; }
		public byte[] abValidationHash { get; private set; }

		internal Password(string sData, byte[] abValidationHash) 
		{
			this.sData = sData;
			this.abValidationHash = abValidationHash;
		}

		internal Password(string sData, string sValidationHash) 
		{
			this.sData = sData;
			abValidationHash = Encoding.UTF8.GetBytes(sValidationHash);
		}
	}
}
