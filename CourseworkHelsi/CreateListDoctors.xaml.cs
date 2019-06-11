using Bogus;
using System;
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
        
        public CreateListDoctors()
        {
            InitializeComponent();
        }

        private void BtnCreateNameDoctors_Click(object sender, RoutedEventArgs e)
        {
            string dbName = txtNameBD.Text;
            SQLiteConnection con = new SQLiteConnection($"Data Source={dbName}");
            con.Open();
            GenerateTabels(con);
            Seed(con);
            con.Close();
        }


        private void GenerateTabels(SQLiteConnection con)
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
        private void Seed(SQLiteConnection con)
        {
            #region SeedNameDoctors         
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
               // MessageBox.Show(first_name + last_name + birthday);
                string query = $"Insert into {tblNameDoctors}(Lastname, Firstname, Birthday) " +
                      $"values('{first_name}','{last_name}','{birthday}');";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.ExecuteNonQuery();
                cmd.Cancel();             
            }
            #endregion


        }
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
