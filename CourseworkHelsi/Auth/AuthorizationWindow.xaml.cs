using CourseworkHelsi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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

                    //UserInfo userInfo = new UserInfo();
                    //userInfo.InfoUser(lgn);
                    //userInfo.ShowDialog();
                    //userInfo.Close();

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
    }
}
