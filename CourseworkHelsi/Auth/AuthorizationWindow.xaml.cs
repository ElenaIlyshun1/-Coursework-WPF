using CourseworkHelsi.Helpers;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace CourseworkHelsi.Auth
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (config.AppSettings.Settings["setLang"].Value == "ru-RU")
            {
                m_RadioButton2.IsChecked = true;
            }
            else if (config.AppSettings.Settings["setLang"].Value == "en")
            {
                m_RadioButton3.IsChecked = true;
            }
            else if (config.AppSettings.Settings["setLang"].Value == "uk")
            {
                m_RadioButton1.IsChecked = true;
            }
        }

        private void Btn_regis_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registration = new RegistrationWindow();

            registration.ShowDialog();
        }
        private void Btn_auth_Click(object sender, RoutedEventArgs e)
        {
            PasswordBox pwBox = txt_password as PasswordBox;
            var pswd = PasswdEncoding.GetPswdMD5(pwBox.Password);
            try
            {
                XDocument doc = XDocument.Load("Users.xml");
                var lgn = doc.Element("USERS").Elements("User")
                                             .Where(user => user.Element("Login").Value == txt_email.Text).First();
                var buf = lgn.Element("Password").Value.ToString();
                if ((buf == pswd))
                {
                    MessageBox.Show("LogIn successful", "Congatulations!", MessageBoxButton.OK, MessageBoxImage.Information);

                    CreateListDoctors userInfo = new CreateListDoctors();
                    userInfo.ShowDialog();
                    userInfo.Close();

                }
                else
                {
                    MessageBox.Show("Wrong user name or password", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch
            {
                MessageBox.Show("Wrong user name or password", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        
        private void Restart()
        {
           
            System.Diagnostics.Process
                .Start(Application.ResourceAssembly.Location);
            //Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            Application.Current.Shutdown();
        }

        private void M_RadioButton2_Click(object sender, RoutedEventArgs e)
        {
           
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["setLang"].Value = "ru-RU";
            config.Save(ConfigurationSaveMode.Modified);
            this.Restart();
        }

        private void M_RadioButton1_Click(object sender, RoutedEventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["setLang"].Value = "uk";
            config.Save(ConfigurationSaveMode.Modified);
            this.Restart();
        }

        private void M_RadioButton3_Click(object sender, RoutedEventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["setLang"].Value = "en";
            config.Save(ConfigurationSaveMode.Modified);
            this.Restart();
        }
    }
}
