using PasswordGenerator.Generator;
using PasswordGenerator.src;
using System.Diagnostics;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private generateString m_generateString;
        private check m_check;

        public MainWindow()
        {
            m_generateString = new generateString();
            m_check = new check();

            InitializeComponent();
        }

        private void generateString()
        {
            string generatedString = "";

            var input = InputBox.Text.ToString();

            int length = Int32.Parse(input);

            generatedString = m_generateString.generateRandomString(length);

            if (m_check.checkPW(generatedString))
            {
                GeneratedLabel.Text = generatedString;
            }
            else
            {
                generateString();
            }
        }

        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            generateString();
        }
    }
}