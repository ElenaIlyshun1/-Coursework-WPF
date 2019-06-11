using CourseworkHelsi.Helpers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace CourseworkHelsi.Auth
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void Checking_Incoming_Data(TextCompositionEventArgs e)
        {
            if (!(Char.IsLetter(e.Text, 0)))
            {
                popup1.IsOpen = true;
                e.Handled = true;
            }
            else
            {
                popup1.IsOpen = false;
            }
        }
        private void Txt_FirstName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Checking_Incoming_Data(e);
        }
        private void Txt_LastName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Checking_Incoming_Data(e);
        }
        private void Txt_PhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == "+")
              && (!txt_PhoneNumber.Text.StartsWith("+")
              && txt_PhoneNumber.Text.Length == 0)))
            {
                popup1.IsOpen = true;
                e.Handled = true;
            }
            else
            {
                popup1.IsOpen = false;
            }
        }

        private void Btn_Cencel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Btn_Regis_Click(object sender, RoutedEventArgs e)
        {
            bool email = true;

            string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            bool isItEmail = Regex.IsMatch(txt_login.Text, emailPattern);

            if (string.IsNullOrWhiteSpace(txt_FirstName.Text) ||
                string.IsNullOrWhiteSpace(txt_LastName.Text) ||
                string.IsNullOrWhiteSpace(txt_PhoneNumber.Text) ||
                string.IsNullOrWhiteSpace(txt_Adressa.Text))
                MessageBox.Show("Заполните пожалуйста ВСЕ поля формы!");
            else if (!isItEmail)
            {
                email = false;
                txt_login.Clear();
                txt_login.Focus();
                MessageBox.Show("Укажите логин в форме х@x.x");
            }
            else
            {
                bool en = true;  // английская раскладка
                bool secure = true;
                bool symbol = false; // символ
                bool number = false; // цифра
                if (Password.Password.Length >= 6)
                {
                    for (int i = 0; i < Password.Password.Length; i++) // перебираем символ
                    {
                        if (Password.Password[i] > 'А' && Password.Password[i] > 'Я') en = false; // если русская раскладка
                        if (Password.Password[i] > '0' && Password.Password[i] < '9') number = true; // если цифры
                        if (Password.Password[i] == '_' || Password.Password[i] == '-' || Password.Password[i] == '!') symbol = true; // если символ
                    }

                    if (!en)
                        MessageBox.Show("Доступна только английская раскладка"); // выводим сообщение
                    else if (!symbol)
                        MessageBox.Show("Добавьте один из следующих символов: _ - ( ) *!"); // выводим сообщение
                    else if (!number)
                        MessageBox.Show("Добавьте хотя бы одну цифру");
                }
                else
                {
                    secure = false;
                    MessageBox.Show("Пароль слишком короткий, минимум 6 символов");
                }

                if (!en || !symbol || !number || !secure || !email)
                {
                    if (!email)
                    {
                        txt_login.Clear();
                        txt_login.Focus();
                    }
                    else
                    {
                        Password.Clear();
                        Password_Copy.Clear();
                        Password.Focus();
                    }
                }
                else
                {
                    if (Password.Password == Password_Copy.Password) // проверка на совпадение паролей
                    {
                        UserRegistration();
                    }
                    else
                    {
                        Password_Copy.Clear();
                        Password_Copy.Focus();
                        MessageBox.Show("Пароли не совподают");
                    }
                }
            }
        }

        private void UserRegistration()
        {
            if (!File.Exists("Users.xml"))
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;

                using (XmlWriter xmlWriter = XmlWriter.Create("Users.xml", xmlWriterSettings))
                {

                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("USERS");

                    xmlWriter.WriteStartElement("User");
                    xmlWriter.WriteElementString("Login", txt_login.Text);
                    PasswordBox pwBox = Password;
                    var pswd = PasswdEncoding.GetPswdMD5(pwBox.Password);
                    xmlWriter.WriteElementString("Password", pswd);
                    xmlWriter.WriteElementString("Name", txt_FirstName.Text);
                    xmlWriter.WriteElementString("Surname", txt_LastName.Text);
                    xmlWriter.WriteElementString("Phone", txt_PhoneNumber.Text);
                    xmlWriter.WriteElementString("Address", txt_Adressa.Text);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();

                    xmlWriter.Close();
                }
                MessageBox.Show("Пользователь успешно добавлен!", "Congatulations!", MessageBoxButton.OK, MessageBoxImage.Information);

                Close();
            }
            else
            {

                XDocument doc = XDocument.Load("Users.xml");
                XElement users = doc.Element("USERS");
                var a = doc.Element("USERS").Elements("User").Where(user => user.Element("Login").Value == txt_login.Text).Any();
                if (!a)
                {
                    PasswordBox pwBox = Password;
                    var pswd = PasswdEncoding.GetPswdMD5(pwBox.Password);
                    users.Add
                       (
                       new XElement("User", new XElement("Login", txt_login.Text), new XElement("Password", pswd),
                       new XElement("Name", txt_FirstName.Text), new XElement("Surname", txt_LastName.Text),
                       new XElement("Phone", txt_PhoneNumber.Text), new XElement("Address", txt_Adressa.Text))
                       );
                    doc.Save("Users.xml");
                    MessageBox.Show("Пользователь успешно добавлен!", "Congatulations!", MessageBoxButton.OK, MessageBoxImage.Information);

                    Close();
                }
                else
                {
                    MessageBox.Show("Пользователь з таким EMAIL уже существует!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
        }
    }
}
