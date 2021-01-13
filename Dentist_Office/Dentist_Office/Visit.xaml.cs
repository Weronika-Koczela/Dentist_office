using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Logika interakcji dla klasy Visit.xaml
    /// </summary>
    public partial class Visit : Window
    {

        public Visit()
        {
            InitializeComponent();


        }


        private void buttonkalendarz_Click(object sender, RoutedEventArgs e)
        {
            if (login.Text.Length != 11)
            {
                MessageBox.Show("Wprowadź pesel pacjenta");
            }
            else
            {
                sprawdzaniegodzin();
            }

        }
        void sprawdzaniegodzin()

        {
            if (kalendarz.SelectedDate.HasValue)
            {
                List<string> lista_godzin = new List<string>();
                int[] tablica_godzin = new int[4];
                tablica_godzin[0] = 12;
                tablica_godzin[1] = 14;
                tablica_godzin[2] = 16;
                tablica_godzin[3] = 18;
                string query = $"SELECT Godzina FROM kalendarz WHERE Data = '{kalendarz.SelectedDate.Value.ToString("yyyy-MM-dd")}';";
                string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=dentysta;";//polaczenie z DB
                MySqlConnection Connection = new MySqlConnection(connection);
                Connection.Open();
                MySqlCommand CommandSQL = Connection.CreateCommand();
                CommandSQL.CommandText = "Select * from kalendarz";//zapytanie do bazy
                MySqlDataReader Reader = CommandSQL.ExecuteReader();
                Reader.Close();
                for (int i = 0; i < 4; i++)
                {
                    CommandSQL.CommandText = $"SELECT Godzina FROM kalendarz WHERE Data = '{kalendarz.SelectedDate.Value.ToString("yyyy-MM-dd")}' AND Godzina = '{tablica_godzin[i].ToString()}0000'";
                    Reader = CommandSQL.ExecuteReader();    //wpisujemy 36 zebow do DB
                    if (Reader.HasRows)
                    {

                    }
                    else
                    {
                        lista_godzin.Add(tablica_godzin[i].ToString() + ":00");
                    }

                    Reader.Close();
                }
                if (lista_godzin.Count < 1)
                {
                    lista_godzin.Add("Brak wolnych godzin w tym dniu");
                }

                lista.ItemsSource = lista_godzin;

                Connection.Close();
            }
            else
            {
                MessageBox.Show("Błąd podczas wybierania daty");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (lista.SelectedItem != null)
            {
                string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=dentysta;";//polaczenie z DB
                MySqlConnection Connection = new MySqlConnection(connection);
                Connection.Open();
                MySqlCommand CommandSQL = Connection.CreateCommand();
                CommandSQL.CommandText = $"Select ID_uzytkownika from uzytkownik where PESEL = '{login.Text}' ";
                MySqlDataReader Reader = CommandSQL.ExecuteReader();
                Reader.Read();
                int idpacjenta=0;
                try { idpacjenta = Reader.GetInt32(0); }
                catch(MySqlException a)
                {
                    MessageBox.Show("Brak użytkownika w bazie o takim peselu");
                }
                Reader.Close();
                if (idpacjenta < 1)
                {
                    Connection.Close();
                    
                }
                else
                {
                    CommandSQL.CommandText = "Select max(ID_terminu) from kalendarz";//zapytanie do bazy
                    Reader = CommandSQL.ExecuteReader();
                    Reader.Read();
                    int maxidterminu = Reader.GetInt32(0);
                    Reader.Close();
                    CommandSQL.CommandText = "Select max(ID_Wizyty) from wizyta";
                    Reader = CommandSQL.ExecuteReader();
                    Reader.Read();
                    int maxidwizyty = Reader.GetInt32(0);
                    Reader.Close();
                    CommandSQL.CommandText = "Select ID_uzytkownika from uzytkownik where status = '3'";
                    Reader = CommandSQL.ExecuteReader();
                    Reader.Read();
                    int idlekarza = Reader.GetInt32(0);
                    Reader.Close();
                    CommandSQL.CommandText = "Select ID_uzytkownika from uzytkownik where status = '2'";
                    Reader = CommandSQL.ExecuteReader();
                    Reader.Read();
                    int idrejestratora = Reader.GetInt32(0);
                    Reader.Close();
                    CommandSQL.CommandText = $"INSERT INTO kalendarz (ID_terminu, Data, Godzina) VALUES ('{maxidterminu + 1}', '{kalendarz.SelectedDate.Value.ToString("yyyy-MM-dd")}', '{lista.SelectedItem}:00')";
                    Reader = CommandSQL.ExecuteReader();
                    Reader.Close();
                    CommandSQL.CommandText = $"INSERT INTO wizyta (ID_Wizyty, ID_lekarza, ID_terminu, ID_Pacjenta, ID_rejestratora, Status_wizyty) VALUES ('{maxidwizyty + 1}', '{idlekarza}', '{maxidterminu}', '{idpacjenta}', '{idrejestratora}', 'Niepotwierdzona')";
                    Reader = CommandSQL.ExecuteReader();
                    MessageBox.Show("Wizyta została dodana");
                    Thread.Sleep(3000);
                    Connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Nie wybrano godziny");
            }
        }

    }
}
