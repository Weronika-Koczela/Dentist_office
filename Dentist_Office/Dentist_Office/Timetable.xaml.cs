using Dentist_Office.Models;
using MySql.Data.MySqlClient;
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

namespace Dentist_Office
{
    /// <summary>
    /// Logika interakcji dla klasy Timetable.xaml
    /// </summary>
    public partial class Timetable : Window
    {
        public Timetable()
        {
            InitializeComponent();
        }
		List<Userfortimetable> items = new List<Userfortimetable>();
		public void ListViewGridViewSample()
		{
			string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=dentysta;";//polaczenie z DB
			MySqlConnection Connection = new MySqlConnection(connection);
			Connection.Open();
			MySqlCommand CommandSQL = Connection.CreateCommand();
			CommandSQL.CommandText = $"SELECT k.Data, k.Godzina, u.Imie, u.Nazwisko, u.PESEL, w.Status_wizyty, w.ID_Wizyty FROM wizyta as w LEFT JOIN kalendarz as k ON k.ID_terminu = w.ID_terminu LEFT JOIN uzytkownik as u ON u.ID_uzytkownika = w.ID_Pacjenta ORDER BY k.Data ASC ";
			MySqlDataReader Reader = CommandSQL.ExecuteReader();
			
			if (Reader.HasRows)
            {
				while(Reader.Read())
                {
					items.Add(new Userfortimetable() { Data = Reader["Data"].ToString(), Godzina = Reader["Godzina"].ToString(), Imie = Reader["Imie"].ToString(), Nazwisko = Reader["Nazwisko"].ToString(), Pesel = Reader["PESEL"].ToString(), Status_Wizyty = Reader["Status_wizyty"].ToString(), Id_wizyty = Reader["ID_Wizyty"].ToString()});
				}
            }
			Reader.Close();
			lvUsers.ItemsSource = items;
			Connection.Close();
		}

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
			ListViewGridViewSample();
        }

		private void Potwierdz(object sender, RoutedEventArgs e)
		{
			if (lvUsers.SelectedValue != null)
			{
                string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=dentysta;";//polaczenie z DB
                MySqlConnection Connection = new MySqlConnection(connection);
                Connection.Open();
                MySqlCommand CommandSQL = Connection.CreateCommand();
                CommandSQL.CommandText = $"UPDATE wizyta SET Status_wizyty = 'Potwierdzona' WHERE wizyta.ID_Wizyty = {lvUsers.SelectedItem.ToString()};";
                MySqlDataReader Reader = CommandSQL.ExecuteReader();
               
                Reader.Close();
				Connection.Close();
				Timetable test = new Timetable();
				test.Show();
				Close();
			}
			else
            {
				MessageBox.Show("Nie zaznaczono wizyty");
            }
		}

        private void Usun(object sender, RoutedEventArgs e)
        {

			if (lvUsers.SelectedValue != null)
			{
				string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=dentysta;";//polaczenie z DB
				MySqlConnection Connection = new MySqlConnection(connection);
				Connection.Open();
				MySqlCommand CommandSQL = Connection.CreateCommand();
				CommandSQL.CommandText = $"SELECT ID_terminu FROM wizyta WHERE wizyta.ID_Wizyty = {lvUsers.SelectedItem.ToString()};";
				MySqlDataReader Reader = CommandSQL.ExecuteReader();
				Reader.Read();
				int id_terminu = Reader.GetInt32(0);
				Reader.Close();
				CommandSQL = Connection.CreateCommand();
				CommandSQL.CommandText = $"DELETE FROM wizyta WHERE wizyta.ID_Wizyty = {lvUsers.SelectedItem.ToString()};";
				Reader = CommandSQL.ExecuteReader();
				Reader.Close(); CommandSQL = Connection.CreateCommand();
				CommandSQL.CommandText = $"DELETE FROM kalendarz WHERE kalendarz.ID_terminu = {id_terminu};";
				Reader = CommandSQL.ExecuteReader();
				Reader.Close();
				Connection.Close();
				Timetable test = new Timetable();
				test.Show();
				Close();
			}
			else
			{
				MessageBox.Show("Nie zaznaczono wizyty");
			}
		}
    }

    public class Userfortimetable
	{
		public string Data { get; set; }
		public string Godzina { get; set; }
		public string Imie { get; set; }
		public string Nazwisko { get; set; }
		public string Pesel { get; set; }
		public string Status_Wizyty { get; set; }
		public string Id_wizyty { get; set; }
		public string Id_terminu { get; set; }
        public override string ToString()
        {
			return Id_wizyty;
        }
    }

}

