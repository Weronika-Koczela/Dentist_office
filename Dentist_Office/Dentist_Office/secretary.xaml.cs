using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace Dentist_Office
{
    /// <summary>
    /// Logika interakcji dla klasy secretary.xaml
    /// </summary>
    public partial class secretary : Window
    {
        public secretary()
        {
            InitializeComponent();
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Registern r = new Registern();
            r.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Visit v = new Visit();
            v.Show();
        }

         void VisitsToday()
        {
            try
            {


                string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=dentysta;";//polaczenie z DB
                string query = "SELECT k.Godzina, u.Imie, u.Nazwisko FROM wizyta as w LEFT JOIN kalendarz as k ON k.ID_terminu = w.ID_terminu LEFT JOIN uzytkownik as u ON u.ID_uzytkownika = w.ID_Pacjenta WHERE k.Data = CURRENT_DATE();";

                MySqlConnection Connection = new MySqlConnection(connection);
                Connection.Open();

                MySqlDataAdapter AdapterSQL = new MySqlDataAdapter();
                AdapterSQL.SelectCommand = new MySqlCommand(query, Connection);
                MySqlCommandBuilder builder = new MySqlCommandBuilder(AdapterSQL);
                DataTable dane = new DataTable();
                AdapterSQL.Fill(dane);
                DzisiejszeWizyty.ItemsSource = dane.DefaultView;
                AdapterSQL.Update(dane);

                Connection.Close();


            }

            catch (MySqlException)
            {

                MessageBox.Show("Bład połączenia z bazą danych", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Wystąpił nieoczekiwany błąd", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            VisitsToday();
        }



        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Timetable t = new Timetable();
            t.Show();
        }
    }
}
