namespace PasswordChecker
{
	public static class PasswordChecker
	{
		private const bool bGenerateNumbers = true;
		private const bool bGenerateSymbols = true;

		public static void Main()
		{
			int nFlag = 1;
			if (bGenerateNumbers) {
				nFlag = nFlag << 1;
			}
			if (bGenerateSymbols) {
				nFlag = nFlag << 2;
			}

			Console.WriteLine(nFlag);
		}
	}
}