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
           // GenerateTabels(con);
          //  Seed(con);
            con.Close();
        }
    }
}
