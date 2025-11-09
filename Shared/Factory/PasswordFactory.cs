using Shared.Models;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Management;
using System.Text;
using System.Runtime.CompilerServices;

namespace Shared.Factory
{
	public class PasswordFactory
	{
//Members--------------------------------------------------------------------------------------------------------------------------------------------------

		private const string c_sWin32 = "win32_processor";
		private const string c_sProcessorID = "processorID";
		private const string c_sThermalZones1 = @"root\WMI" ;
		private const string c_sThermalZones2 = "SELECT * FROM MSAcpi_ThermalZoneTemperature";
		private const string c_sTemperature = "CurrentTemperature";

//Singleton------------------------------------------------------------------------------------------------------------------------------------------------

		private static PasswordFactory? m_oInstance;

		private PasswordFactory()
		{
			
		}

		public static PasswordFactory Instance
		{
			get {
				if(m_oInstance == null){
					m_oInstance = new PasswordFactory();
				}

				return m_oInstance;
			}
		}
//Methods------------------------------------------------------------------------------------------------------------------------------------------------
		private byte[] GenerateValidationHash(byte[] abData)
		{
			byte[] abResult;

			abResult = HashingProxy.Instance.GetNewestSHAAlgorithm().HashData(abData);

			return abResult;
		}

		private byte[] GenerateValidationHash(string sData)
		{
			byte[] abResult;

			abResult = HashingProxy.Instance.GetNewestSHAAlgorithm().HashData(Encoding.UTF8.GetBytes(sData));

			return abResult;
		}

/// <summary>
/// Shuffles the input data using the fisher yates algorithm
/// </summary>
/// <param name="abData"></param>
/// <returns>The data shuffled</returns>
		private byte[] ShuffleBytes(byte[] abData)
		{
			int nRand;
			byte bTemp;

			for (int i = 0; i < abData.Length -1; i++) {
				nRand = RandomNumberGenerator.GetInt32(0, i + 1);
				bTemp = abData[i];
				abData[i] = abData[nRand];
				abData[nRand] = bTemp;
			}

			return abData;
		}

/// <summary>
/// Shuffles the input data using the fisher yates algorithm
/// </summary>
/// <param name="sData"></param>
/// <returns>The data shuffled</returns>
		[Obsolete]
		private string ShuffleString(string sData)
		{
			char[] acData = new char[sData.Length];
			int nRand;
			char cTemp;

			for (int i = 0; i < sData.Length -1; i++) {
				nRand = RandomNumberGenerator.GetInt32(0, i + 1);
				//cTemp = sData[i];
				//sData[i] = sData[nRand];
				//sData[nRand] = cTemp;
			}

			return new string(acData);
		}

/// <summary>
/// Generates some random data using some random input data
/// </summary>
/// <returns>A byte[] of the acumalated data after a scrambling run</returns>
		private byte[] GenerateData()
		{
			byte[] abResult;
			StringBuilder oStringBuilder = new StringBuilder();

			Point oMousePosition = GetCursorPos();
			byte[]? abHardwareID = GetHardwareID();
			double fCPUTemp = GetCPUTemp();
			long nTimeInUTC = GetCurrentTime();

			oStringBuilder.Append(Encoding.UTF8.GetString(BitConverter.GetBytes(oMousePosition.x)));
			oStringBuilder.Append(Encoding.UTF8.GetString(BitConverter.GetBytes(oMousePosition.y)));
			oStringBuilder.Append(Encoding.UTF8.GetString(abHardwareID));
			oStringBuilder.Append(Encoding.UTF8.GetString(BitConverter.GetBytes(fCPUTemp)));
			oStringBuilder.Append(Encoding.UTF8.GetString(BitConverter.GetBytes(nTimeInUTC)));

			abResult = Encoding.UTF8.GetBytes(oStringBuilder.ToString());
			abResult = ShuffleBytes(abResult);

			abResult = GenerateValidationHash(abResult);

			return abResult;
		}

		private string GeneratePasswordWithCharacters(int nLength)
		{
			StringBuilder oStringBuilder = new StringBuilder();
			byte[] abData = GenerateData();

			char cCharacter;
			for (int i = 0; i <= nLength; i++) {
				cCharacter = Convert.ToChar(abData[i]);
				if (!char.IsNumber(cCharacter) && !char.IsSymbol(cCharacter)) {
					oStringBuilder.Append(cCharacter);
				}
			}

			return oStringBuilder.ToString();
		}

		private string GeneratePasswordWithNumbers(int nLength)
		{
			StringBuilder oStringBuilder = new StringBuilder();
			byte[] abData = GenerateData();

			char cCharacter;
			for (int i = 0; i <= nLength; i++) {
				cCharacter = Convert.ToChar(abData[i]);
				if (!char.IsSymbol(cCharacter)) {
					oStringBuilder.Append(cCharacter);
				}
			}

			return oStringBuilder.ToString();
		} 

		private string GeneratePasswordWithSymbols(int nLength)
		{
			StringBuilder oStringBuilder = new StringBuilder();
			byte[] abData = GenerateData();

			char cCharacter;
			for (int i = 0; i <= nLength; i++) {
				cCharacter = Convert.ToChar(abData[i]);
				if (!char.IsNumber(cCharacter) && !char.IsSymbol(cCharacter)) {
					oStringBuilder.Append(cCharacter);
				}
			}

			return oStringBuilder.ToString();
		} 

		private string GeneratePasswordWithNumbersAndSymbols(int nLength)
		{
			StringBuilder oStringBuilder = new StringBuilder();
			byte[] abData = GenerateData();

			char cCharacter;
			for (int i = 0; i <= nLength; i++) {
				cCharacter = Convert.ToChar(abData[i]);
				oStringBuilder.Append(cCharacter);
			}

			return oStringBuilder.ToString();
		}

		public Password GeneratePassword(int nLength, bool bGenerateSymbols = false, bool bGenerateNumbers = false) 
		{
			Password oResult;

			string sData;
			byte[] abValidationHash;

			int nFlag = 1;
			if (bGenerateNumbers) {
				nFlag = nFlag << 1;
			}
			if (bGenerateSymbols) {
				nFlag = nFlag << 2;
			}
		
			switch (nFlag) {
				case 2:
					sData = GeneratePasswordWithNumbers(nLength);
					break;
				case 4:
					sData = GeneratePasswordWithSymbols(nLength);
					break;
				case 8:
					sData = GeneratePasswordWithNumbersAndSymbols(nLength);
					break;
				default:
					sData = GeneratePasswordWithCharacters(nLength);
					break;
			}

			abValidationHash = GenerateValidationHash(sData);

			oResult = new Password(sData, abValidationHash);

			return oResult;
		}
//Helpers-----------------------------------------------------------------------------------------------------------------------------------------------

		[DllImport("user32.dll")]
		private static extern bool GetCursorPos(out Point lpPoint);

		private static Point GetCursorPos()
		{
			Point lpPoint;
			GetCursorPos(out lpPoint);

			return lpPoint;
		}

		private byte[]? GetHardwareID()
		{
			byte[]? abResult = null;

			ManagementClass oMan = new ManagementClass(c_sWin32);
			ManagementObjectCollection oManCollection = oMan.GetInstances();

			foreach (var oObject in oManCollection) {
				abResult = Encoding.UTF8.GetBytes(oObject.Properties[c_sProcessorID].Value.ToString());
				break;
			}

			return abResult;
		}

		private double GetCPUTemp()
		{
			double fCurrentTemp = 0.0f;

			//ManagementObjectSearcher oMan = new ManagementObjectSearcher(c_sThermalZones1, c_sThermalZones2);

			//foreach (ManagementObject oObject in oMan.Get()) {
			//	fCurrentTemp = Convert.ToDouble(oObject[c_sTemperature].ToString());
			//	break;
			//}

			return fCurrentTemp;
		}

		private long GetCurrentTime()
		{
			return System.DateTime.Now.ToFileTimeUtc();
		}
	}
}
