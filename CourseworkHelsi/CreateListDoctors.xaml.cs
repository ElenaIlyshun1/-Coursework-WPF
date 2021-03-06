﻿using Bogus;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SQLite;
using System.Windows;

namespace CourseworkHelsi
{
    /// <summary>
    /// Interaction logic for CreateListDoctors.xaml
    /// </summary>
    public partial class CreateListDoctors : Window
    {
        private string tblNameDoctors = "tblNameDoctors";
        private string tblNameCity = "tblNameCity";
        private string tblNameClinic = "tblNameClinic";
        private string tblNameClients = "tblNameClients";
        private string tblNameSpecialization = "tblNameSpecialization";
        private string tblConnectionTables = "tblConnectionTables";

        public CreateListDoctors()
        {
            InitializeComponent();
            string dbName = txtNameBD.Text;
            SQLiteConnection con = new SQLiteConnection($"Data Source={dbName}");
            con.Open();
            string query = "PRAGMA foreign_keys = ON";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        #region SeedNameDoctors         
        private void BtnCreateNameDoctors_Click(object sender, RoutedEventArgs e)
        {
            string dbName = txtNameBD.Text;
            SQLiteConnection con = new SQLiteConnection($"Data Source={dbName}");
            con.Open();
            GenerateTabelDoctors(con);
            SeedDoctors(con);
            con.Close();
        }
        private void GenerateTabelDoctors(SQLiteConnection con)
        {
            string query = $"CREATE TABLE IF NOT EXISTS {tblNameDoctors} " +
                                "(Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                    "Lastname TEXT NOT NULL, " +
                                    "Firstname TEXT, " +
                                    "Birthday DATE" +
                                ");";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
        }
        private void SeedDoctors(SQLiteConnection con)
        {
            var userFaker = new Faker<DoctorsService>("uk")
               .RuleFor(o => o.Name, f => f.Name.FirstName())
               .RuleFor(o => o.LastName, f => f.Name.LastName())
               .RuleFor(o => o.Birthday, f => f.Date.Past());
            var list = userFaker.Generate(500);
            foreach (var user in list)
            {
                string first_name = user.Name;
                string last_name = user.LastName;
                var birthday = user.Birthday;
               // MessageBox.Show(first_name + last_name + birthday);
                string query = $"Insert into {tblNameDoctors}(Lastname, Firstname, Birthday) " +
                      $"values('{first_name}','{last_name}','{birthday}');";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
        #endregion
        #region SeedNameCity  
        private void BtnCreateTableCity_Click(object sender, RoutedEventArgs e)
        {
            string dbName = txtNameBD.Text;
            SQLiteConnection con = new SQLiteConnection($"Data Source={dbName}");
            con.Open();
            GenerateTabelCity(con);
            SeedCity(con);
            con.Close();
        }
        private void GenerateTabelCity(SQLiteConnection con)
        {
            string query = $"CREATE TABLE IF NOT EXISTS {tblNameCity} " +
                                "(Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                    "City TEXT NOT NULL" +
                                ");";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
        }
        private void SeedCity(SQLiteConnection con)
        {
            var userFaker = new Faker<DoctorsService>("uk")
               .RuleFor(o => o.City, f => f.Address.City());

            var list = userFaker.Generate(10);
            foreach (var user in list)
            {
                string city = user.City;
                string query = $"Insert into {tblNameCity} (City) " +
                      $"values('{city}');";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
        #endregion
        #region SeedNameClinic      
        private void BtnCreateTableClinic_Click(object sender, RoutedEventArgs e)
        {
            string dbName = txtNameBD.Text;
            SQLiteConnection con = new SQLiteConnection($"Data Source={dbName}");
            con.Open();
            GenerateTabelClinic(con);
            SeedClinic(con);
            con.Close();
        }
        private void GenerateTabelClinic(SQLiteConnection con)
        {
            string query = $"CREATE TABLE IF NOT EXISTS {tblNameClinic} " +
                                "(Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                    "Clinic TEXT NOT NULL, " +
                                    "Street TEXT NOT NULL" +
                                ");";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
        }
        private void SeedClinic(SQLiteConnection con)
        {
            var userFaker = new Faker<DoctorsService>("uk")
               .RuleFor(o => o.Clinic, f => f.Company.CompanyName())
               .RuleFor(o => o.Street, f => f.Address.StreetAddress());

            var list = userFaker.Generate(50);
            foreach (var user in list)
            {
                string clinic = user.Clinic;
                string street = user.Street;

                string query = $"Insert into {tblNameClinic} (Clinic, Street) " +
                      $"values('{clinic}','{street}');";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
        #endregion
        #region SeedNameClients 
        private void BtnCreateTableClients_Click(object sender, RoutedEventArgs e)
        {
            string dbName = txtNameBD.Text;
            SQLiteConnection con = new SQLiteConnection($"Data Source={dbName}");
            con.Open();
            GenerateTabelClients(con);
            SeedClients(con);
            con.Close();
        }
        private void GenerateTabelClients(SQLiteConnection con)
        {
            string query = $"CREATE TABLE IF NOT EXISTS {tblNameClients} " +
                                "(Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                    "Lastname TEXT NOT NULL, " +
                                    "Firstname TEXT, " +
                                    "Birthday DATE" +
                                ");";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
        }
        private void SeedClients(SQLiteConnection con)
        {
            var userFaker = new Faker<DoctorsService>("uk")
               .RuleFor(o => o.Name, f => f.Name.FirstName())
               .RuleFor(o => o.LastName, f => f.Name.LastName())
               .RuleFor(o => o.Birthday, f => f.Date.Past());
            var list = userFaker.Generate(1000);
            foreach (var user in list)
            {
                string first_name = user.Name;
                string last_name = user.LastName;
                var birthday = user.Birthday;
                string query = $"Insert into {tblNameClients}(Lastname, Firstname, Birthday) " +
                      $"values('{first_name}','{last_name}','{birthday}');";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
        #endregion
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            string dbName = txtNameBD.Text;
            SQLiteConnection con = new SQLiteConnection($"Data Source={dbName}");
            con.Open();
            string query = $"DELETE FROM {tblNameCity}";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();

            query = $"DELETE FROM {tblNameClinic}";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"DELETE FROM {tblNameDoctors}";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"DELETE FROM {tblNameSpecialization}";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"DELETE FROM {tblNameClients}";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"DELETE FROM {tblConnectionTables}";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            cmd.Cancel();
            con.Close();
        }

        #region SeedNameSpecialization
        private void BtnCreateSpecialization_Click(object sender, RoutedEventArgs e)
        {
            string dbName = txtNameBD.Text;
            SQLiteConnection con = new SQLiteConnection($"Data Source={dbName}");
            con.Open();
            GenerateTabelSpecialization(con);
            SeedSpecialization(con);
            con.Close();
        }
        private void GenerateTabelSpecialization(SQLiteConnection con)
        {
            string query = $"CREATE TABLE IF NOT EXISTS {tblNameSpecialization} " +
                                "(Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                    "Specialization TEXT NOT NULL" +
                                ");";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
        }
        private void SeedSpecialization(SQLiteConnection con)
        {
            string[] specialization = { "Терапевт", "Лор", "Хірург", "Окуліст", "Стоматолог", "Невропатолог", "Дерматолог", "Алерголог", "Гастроентеролог", "Дієтолог", "Ендокринолог", "Імунолог", "Кардіолог", "Нарколог", "Педіатр", "Психіатр" };
            for (int i = 0; i < specialization.Length; i++)
            {
                string query = $"Insert into {tblNameSpecialization} (Specialization) " +
                                     $"values('{specialization[i]}');";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
        #endregion

        #region SeedTabelConnection
        private void BtnCreateLinksByTables_Click(object sender, RoutedEventArgs e)
        {
            string dbName = txtNameBD.Text;
            SQLiteConnection con = new SQLiteConnection($"Data Source={dbName}");
            con.Open();
            GenerateTabelConnection(con);
            SeedConnection(con);
            con.Close();
        }
        private void GenerateTabelConnection(SQLiteConnection con)
        {
            string query = $"CREATE TABLE IF NOT EXISTS {tblConnectionTables} " +
                                "(Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                    "CityId int NOT NULL, " +
                                    "ClinicId int NOT NULL, " +
                                    "DoctorsId int NOT NULL, " +
                                    "SpetealizationId int NOT NULL, " +
                                    "ClientsId int NOT NULL, " +
                                    "FOREIGN KEY (CityId) REFERENCES tblStudents(Id), " +
                                    "FOREIGN KEY (ClinicId) REFERENCES tblSubjects(Id)," +
                                    "FOREIGN KEY (DoctorsId) REFERENCES tblStudents(Id), " +
                                    "FOREIGN KEY (SpetealizationId) REFERENCES tblSubjects(Id)," +
                                    "FOREIGN KEY (ClientsId) REFERENCES tblStudents(Id) " +
                                ");";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
        }
        private void SeedConnection(SQLiteConnection con)
        {
            //string query = query = $"DELETE FROM {tblConnectionTables}";
            string query = $"Insert into {tblConnectionTables} (CityId,ClinicId,DoctorsId,SpetealizationId,ClientsId) values(1,1,10,15,1)";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"Insert into {tblConnectionTables} (CityId,ClinicId,DoctorsId,SpetealizationId,ClientsId) values(1,2,25,1,112)";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"Insert into {tblConnectionTables} (CityId,ClinicId,DoctorsId,SpetealizationId,ClientsId) values(1,3,100,5,58)";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"Insert into {tblConnectionTables} (CityId,ClinicId,DoctorsId,SpetealizationId,ClientsId) values(1,4,77,7,17)";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"Insert into {tblConnectionTables} (CityId,ClinicId,DoctorsId,SpetealizationId,ClientsId) values(1,5,18,4,28)";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"Insert into {tblConnectionTables} (CityId,ClinicId,DoctorsId,SpetealizationId,ClientsId) values(1,2,11,2,25)";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"Insert into {tblConnectionTables} (CityId,ClinicId,DoctorsId,SpetealizationId,ClientsId) values(1,3,19,11,32)";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"Insert into {tblConnectionTables} (CityId,ClinicId,DoctorsId,SpetealizationId,ClientsId) values(1,4,1,6,19)";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            query = $"Insert into {tblConnectionTables} (CityId,ClinicId,DoctorsId,SpetealizationId,ClientsId) values(2,1,18,1,112)";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"Insert into {tblConnectionTables} (CityId,ClinicId,DoctorsId,SpetealizationId,ClientsId) values(2,2,19,8,100)";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"Insert into {tblConnectionTables} (CityId,ClinicId,DoctorsId,SpetealizationId,ClientsId) values(2,3,27,14,99)";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"Insert into {tblConnectionTables} (CityId,ClinicId,DoctorsId,SpetealizationId,ClientsId) values(2,4,32,6,85)";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"Insert into {tblConnectionTables} (CityId,ClinicId,DoctorsId,SpetealizationId,ClientsId) values(2,5,25,15,17)";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"Insert into {tblConnectionTables} (CityId,ClinicId,DoctorsId,SpetealizationId,ClientsId) values(2,5,28,3,45)";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"Insert into {tblConnectionTables} (CityId,ClinicId,DoctorsId,SpetealizationId,ClientsId) values(2,3,33,4,101)";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            //////////////////////////////////////////////////////////////////////////////////////

            query = $"Insert into {tblConnectionTables} (CityId,ClinicId,DoctorsId,SpetealizationId,ClientsId) values(3,1,22,1,25)";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"Insert into {tblConnectionTables} (CityId,ClinicId,DoctorsId,SpetealizationId,ClientsId) values(3,2,12,9,41)";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"Insert into {tblConnectionTables} (CityId,ClinicId,DoctorsId,SpetealizationId,ClientsId) values(3,3,17,16,21)";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"Insert into {tblConnectionTables} (CityId,ClinicId,DoctorsId,SpetealizationId,ClientsId) values(3,4,19,11,17)";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"Insert into {tblConnectionTables} (CityId,ClinicId,DoctorsId,SpetealizationId,ClientsId) values(3,5,11,13,19)";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"Insert into {tblConnectionTables} (CityId,ClinicId,DoctorsId,SpetealizationId,ClientsId) values(3,4,35,8,36)";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"Insert into {tblConnectionTables} (CityId,ClinicId,DoctorsId,SpetealizationId,ClientsId) values(3,2,28,10,14)";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            //////////////////////////////////////////////////////////////////////////////////////////

            query = $"Insert into {tblConnectionTables} (CityId,ClinicId,DoctorsId,SpetealizationId,ClientsId) values(4,2,1,1,1)";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"Insert into {tblConnectionTables} (CityId,ClinicId,DoctorsId,SpetealizationId,ClientsId) values(5,2,1,1,1)";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            cmd.Cancel();
        }
        #endregion
    }
    public class DoctorsService : INotifyPropertyChanged
    {
        
        private string name;

        private string lastname;

        private DateTime birthday;
        public DateTime Birthday
        {
            get { return this.birthday; }
            set
            {
                if (this.birthday != value)
                {
                    this.birthday = value;
                    this.NotifyPropertyChanged("Birthday");
                }
            }
        }
        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.NotifyPropertyChanged("Name");
                }
            }
        }
        public string LastName
        {
            get { return this.lastname; }
            set
            {
                if (this.lastname != value)
                {
                    this.lastname = value;
                    this.NotifyPropertyChanged("LastName");
                }
            }
        }
        public string BirthdayDoctor { get; set; }
        public string City { get; set; }
        public string Clinic { get; set; }

        public string Doctors { get; set; }
        public string Street { get; set; }
        public string Specialization { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public override string ToString() { return City; }
    }
}
