using Shared.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
	internal class HashingProxy
	{
		private static HashingProxy? m_oInstance;

		private HashingProxy()
		{
		
		}

		internal static HashingProxy Instance 
		{
			get {
				if (m_oInstance == null) {
					m_oInstance = new HashingProxy();
				}

				return m_oInstance;
			}
		}

		internal IHashingAlgorithm GetNewestSHAAlgorithm()
		{
			return new Sha256();
		}
	}
}
