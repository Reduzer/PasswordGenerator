using Shared;
using Shared.Factory;
using System.Windows;

namespace PasswordGenerator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private const string c_sMessageNoNumber = "Bitte gebe eine Zahl an!";
		private const string c_sInfoCaption = "Information";

		private Password m_oGeneratedPassword;
		private int m_nLength;

		public MainWindow()
		{
			InitializeComponent();
			Init();
		}

		private void Init()
		{
			GeneratedLabel.IsReadOnly = true;
		}

		private void BtnGenerate_Click(object sender, RoutedEventArgs e)
		{
			int nLength;

			if (!int.TryParse(InputBox.Text, out nLength)) {
				MessageBox.Show(c_sMessageNoNumber, c_sInfoCaption, MessageBoxButton.OK);
			}
			else {
				GeneratePassword(nLength);

				GeneratedLabel.Text = m_oGeneratedPassword.sData;
			}
		}

		private void GeneratePassword(int nLength)
		{
			m_oGeneratedPassword = PasswordFactory.Instance.GeneratePassword(nLength);
		}
	}
}