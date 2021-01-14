using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
        List<Usergetdata> items = new List<Usergetdata>();
        void getdata()
        {
           
           
                string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=dentysta;";//polaczenie z DB
                MySqlConnection Connection = new MySqlConnection(connection);
                Connection.Open();
                MySqlCommand CommandSQL = Connection.CreateCommand();
                CommandSQL.CommandText = $"SELECT imie,nazwisko,PESEL FROM uzytkownik INNER JOIN wizyta on uzytkownik.ID_uzytkownika = wizyta.ID_Pacjenta INNER JOIN kalendarz on wizyta.ID_terminu = kalendarz.ID_terminu WHERE kalendarz.Data = CURRENT_DATE();";
                MySqlDataReader Reader = CommandSQL.ExecuteReader();

                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        items.Add(new Usergetdata() { Imie = Reader["Imie"].ToString(), Nazwisko = Reader["Nazwisko"].ToString(), Pesel = Reader["PESEL"].ToString() });
                    }
                }
                Reader.Close();
                userlist.ItemsSource = items;
                Connection.Close();
            
        }
        public class Usergetdata
        {
           
            public string Imie { get; set; }
            public string Nazwisko { get; set; }
            public string Pesel { get; set; }

            public override string ToString()
            {
                return Pesel;
            }
        }

        private void listloaded(object sender, RoutedEventArgs e)
        {
            getdata();
        }

       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(userlist.SelectedValue!=null)
            {
                string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=dentysta;";//polaczenie z DB
                MySqlConnection Connection = new MySqlConnection(connection);
                Connection.Open();
                MySqlCommand CommandSQL = Connection.CreateCommand();
                CommandSQL.CommandText = $"SELECT Imie from uzytkownik where PESEL = '{userlist.SelectedItem.ToString()}' ";
                MySqlDataReader Reader = CommandSQL.ExecuteReader();
                Reader.Read();
                getname.Content = Reader.GetString(0);
               
                Reader.Close();
                CommandSQL.CommandText = $"SELECT Nazwisko from uzytkownik where PESEL = '{userlist.SelectedItem.ToString()}' ";
                Reader = CommandSQL.ExecuteReader();
                Reader.Read();
                getsurname.Content = Reader.GetString(0);

                Reader.Close();

                getid.Content = userlist.SelectedItem.ToString();

               
                //userlist.ItemsSource = items;
                Connection.Close();
                showteeth();
            }
        }
        List<int> teeth = new List<int>();
        void showteeth()
        {
           
            for (int i = 0; i < 36; i++)
            {
                //teeth.Add(i + 1);
                listteeth.Items.Add(i + 1);
            }
            //listteeth.ItemsSource = teeth;
        }
        private void loadt(object sender, RoutedEventArgs e)
        {

        }

        private void teethcombo(object sender, RoutedEventArgs e)
        {
            showteeth();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(description.Text.Length>1 && listteeth.SelectedItem!=null)
            {
                string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=dentysta;";//polaczenie z DB
                MySqlConnection Connection = new MySqlConnection(connection);
                Connection.Open();
                MySqlCommand CommandSQL = Connection.CreateCommand();
                CommandSQL.CommandText = $"UPDATE zeby INNER JOIN lista_zebow ON lista_zebow.Id_zeba = zeby.ID_zeba Inner join karta_pacjenta on lista_zebow.Id_uzebienie = karta_pacjenta.Id_uzebienie INNER JOIN uzytkownik ON karta_pacjenta.Id_pacjenta = uzytkownik.ID_uzytkownika SET opis = '{description.Text}', Stan = 'zbadany' WHERE PESEL = '{userlist.SelectedItem.ToString()}' AND Oznaczenie = '{ listteeth.SelectedItem.ToString()}'";
                MySqlDataReader Reader = CommandSQL.ExecuteReader();
                

                Reader.Close();

                MessageBox.Show("Dodano opis");


                //userlist.ItemsSource = items;
                Connection.Close();
            }
            else
            {
                MessageBox.Show("Nie dziala");
            }
        }
    }
}