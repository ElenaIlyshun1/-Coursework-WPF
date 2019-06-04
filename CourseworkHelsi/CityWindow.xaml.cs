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
    /// Interaction logic for CityWindow.xaml
    /// </summary>
    public partial class CityWindow : Window
    {
        private string city = "city";
        public CityWindow()
        {
            InitializeComponent();
        }

        private void GenerateTabelForCity(SQLiteConnection con)
        {
            string query = $"CREATE TABLE IF NOT EXISTS {city} " +
                                "(Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                    "City VARCHAR NOT NULL " +
                                ");";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
        }
    }
}
