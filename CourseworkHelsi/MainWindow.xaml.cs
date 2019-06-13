using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseworkHelsi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ObservableCollection<DoctorsService> doctorsServices = new ObservableCollection<DoctorsService>();
        
        public MainWindow()
        {
            InitializeComponent();
            SearchUsers();
            //ListCity.ItemsSource = doctorsServices;

        }

        private void SearchUsers()
        {
            doctorsServices.Clear();
            string dbName = "tblNameDoctors.sqlite";
            string dbName2 = "DataBaseAllTables.sqlite";
            
            SQLiteConnection con = new SQLiteConnection($"Data Source={dbName}");
            con.Open();
            string queryCity = $"SELECT City FROM tblNameCity";
            SQLiteCommand cmdCity = new SQLiteCommand(queryCity, con);
            SQLiteDataReader readerCity = cmdCity.ExecuteReader();
            while (readerCity.Read())
            {
                DoctorsService city = new DoctorsService
                {
                    City = readerCity["City"].ToString(),
                };
                ListBoxItem itm = new ListBoxItem();
                itm.Content = city.City;
                ListCity.Items.Add(itm);
            }

            string queryClinic = $"SELECT Clinic, Street FROM tblNameClinic";
            SQLiteCommand cmdClinic = new SQLiteCommand(queryClinic, con);
            SQLiteDataReader readerClinic = cmdClinic.ExecuteReader();
            while (readerClinic.Read())
            {
                DoctorsService clinic = new DoctorsService
                {
                    Clinic = readerClinic["Clinic"].ToString(),
                    Street = readerClinic["Street"].ToString(),
                };
                ListBoxItem itm = new ListBoxItem();
                itm.Content = clinic.Clinic + " >< " + clinic.Street;
                ListClinic.Items.Add(itm);
            }

            string queryDoctors = $"SELECT Lastname, Firstname, Birthday FROM tblNameDoctors";
            SQLiteCommand cmdDoctors = new SQLiteCommand(queryDoctors, con);
            SQLiteDataReader readerDoctors = cmdDoctors.ExecuteReader();
            while (readerDoctors.Read())
            {
                DoctorsService doctors = new DoctorsService
                {
                    Doctors = readerDoctors["Lastname"].ToString(),
                    Name = readerDoctors["Firstname"].ToString(),
                    // = readerDoctors["Birthday"].ToString(),
                };
                ListBoxItem itm = new ListBoxItem();
                itm.Content = doctors.Doctors + " " + doctors.Name + " " + doctors.BirthdayDoctor;
                ListDoctors.Items.Add(itm);
            }
            

            SQLiteConnection con2 = new SQLiteConnection($"Data Source={dbName2}");
            con2.Open();
            string querySpecialization = $"SELECT Specialization FROM tblNameSpecialization";
            SQLiteCommand cmdSpecialization = new SQLiteCommand(querySpecialization, con2);
            SQLiteDataReader readerSpecialization = cmdSpecialization.ExecuteReader();
            while (readerSpecialization.Read())
            {
                DoctorsService specialization = new DoctorsService
                {
                    Specialization = readerSpecialization["Specialization"].ToString(),
                };
                ListBoxItem itm = new ListBoxItem();
                itm.Content = specialization.Specialization;
                ListSpecialization.Items.Add(itm);
            }

            string queryClients = $"SELECT Lastname, Firstname, Birthday FROM tblNameClients";
            SQLiteCommand cmdClients = new SQLiteCommand(queryClients, con2);
            SQLiteDataReader readerClients = cmdClients.ExecuteReader();
            while (readerClients.Read())
            {
                DoctorsService clients = new DoctorsService
                {
                    ClientsLastName = readerClients["Lastname"].ToString(),
                    ClientsFirstName = readerClients["Firstname"].ToString(),
                    //ClientsBirthday = readerClients["Birthday"].ToString(),
                };
                ListBoxItem itm = new ListBoxItem();
                itm.Content = clients.ClientsLastName + " " + clients.ClientsFirstName + " " + clients.ClientsBirthday;
                ListClients.Items.Add(itm);
            }
            con.Close();
            con2.Close();
        }

        
    }
}
