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
            SQLiteConnection con = new SQLiteConnection($"Data Source={dbName}");
            con.Open();
            string query = $"SELECT City FROM tblNameCity";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            SQLiteDataReader reader = cmd.ExecuteReader();
            List list = new List();
            while (reader.Read())
            {
                DoctorsService city = new DoctorsService
                {
                    City = reader["City"].ToString(),
                };
                ListCity.Items.Add(city.City);
                //doctorsServices.Add(city);
            }
            con.Close();
        }

        
    }
}
