using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

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
            string query = $"Insert into {tblNameDoctors}(Lastname, Firstname, Birthday) " +
                $"values('Мельник', 'Оксана', '2000-01-20');";

            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();

            query = $"Insert into {tblNameDoctors}(Lastname, Firstname, Birthday) " +
                $"values('Шльомов', 'Денис', '1995-03-23');";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            query = $"Insert into {tblNameDoctors}(Lastname, Firstname, Birthday) " +
                $"values('Балконський', 'Андрій', '1985-12-31');";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            #endregion

           
        }
    }
}
