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
	/// Logika interakcji dla klasy DentistWindow.xaml
	/// </summary>
	public partial class DentistWindow : Window
	{
		public DentistWindow()
		{
			InitializeComponent();
		}
		String connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=dentysta;";

		private void Grid_Loaded(object sender, RoutedEventArgs e)
		{
			using (MySqlConnection conn = new MySqlConnection(connectionString))
			{
				string commandStr = "SELECT imie,nazwisko,PESEL FROM uzytkownik  " +
					"INNER JOIN wizyta on uzytkownik.ID_uzytkownika = wizyta.ID_Pacjenta" +
					"INNER JOIN kalendarz on wizyta.ID_terminu = kalendarz.ID_terminu" +
					"WHERE kalendarz.Data = CURRENT_DATE(); ";
				MySqlCommand command = new MySqlCommand(commandStr, conn);
				command.Connection.Open();
				gridDentist.ItemsSource = command.ExecuteReader();
				command.Connection.Close();

			}
		}

		private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

			string fname = gridDentist.SelectedCells[0].ToString();
			string lname = gridDentist.SelectedCells[1].ToString();
			string pesel = gridDentist.SelectedCells[3].ToString();

			setfname.Content = fname;
			setname.Content = lname;
			setpesel.Content = pesel;

			
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			using (MySqlConnection conn = new MySqlConnection(connectionString))
			{

				string commStr = $"Select ID_uzytkownika from uzytkownik where PESEL = '{setpesel.Content}' ";
				MySqlCommand commandGetID = new MySqlCommand(commStr, conn);
				MySqlDataReader reader1 = commandGetID.ExecuteReader();

				commandGetID.Connection.Open();
				reader1.Read();
				commandGetID.ExecuteReader();
				int id; int.TryParse(commStr, out id);
				commandGetID.Connection.Close();

				

				string commandStr = $"INSERT INTO values({chooseTeeth.Text},{id},1) ";
				MySqlCommand command = new MySqlCommand(commandStr, conn);
				MySqlDataReader reader = command.ExecuteReader();

				command.Connection.Open();
				reader.Read();
				command.Connection.Close();
				
			}
		}

		private void ComboBox_Loaded(object sender, RoutedEventArgs e)
		{
			using (MySqlConnection conn = new MySqlConnection(connectionString))
			{
				string commandStr = "SELECT* FROM lista_zebow ";
				MySqlCommand command = new MySqlCommand(commandStr, conn);
				command.Connection.Open();
				gridDentist.ItemsSource = command.ExecuteReader();
				command.Connection.Close();

			}

		}
	}
}
