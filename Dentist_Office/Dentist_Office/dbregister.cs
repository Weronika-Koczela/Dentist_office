using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Data;
using MySql.Data.MySqlClient;
using Dentist_Office.Exceptions;
using System.Threading;

namespace Dentist_Office
{
    class dbregister
    {

        public bool dbtry(string qw, string PESEL)
        {
            try
            {


                string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=dentysta;";//polaczenie z DB
                MySqlConnection Connection = new MySqlConnection(connection);
                Connection.Open();
                MySqlCommand CommandSQL = Connection.CreateCommand();
                CommandSQL.CommandText = $"Select Imie from uzytkownik where PESEL = {PESEL} and status = 1"; //Sprawdzenie czy w DB wystepuje juz taki pesel
                MySqlDataReader Reader = CommandSQL.ExecuteReader();
                if (Reader.HasRows)
                {
                    MessageBox.Show($"{new UserInvalidPESEL()}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
                    Connection.Close();
                    return false;
                }
                else
                {
                    Reader.Close();
                    string max_id_zeba = "Select max(id_zeba) from zeby";
                    CommandSQL.CommandText = max_id_zeba;
                    Reader = CommandSQL.ExecuteReader();
                    Reader.Read();
                    int maxidzeba = Reader.GetInt32(0);
                    Reader.Close();
                    int[] zeby = new int[36];
                    for (int i = 0; i < 36; i++)
                    {
                        zeby[i] = maxidzeba + 1;                //pobieramy max id zeba i tworzymy kolejne 36
                        maxidzeba++;

                    }
                    for (int i = 0; i < 36; i++)
                    {
                        CommandSQL.CommandText = $"INSERT INTO zeby (ID_zeba, Oznaczenie, Stan, Opis, Data_aktualizacji) VALUES('{zeby[i]}', '{i + 1}', 'nieznany', 'niezbadany', current_timestamp())";
                        Reader = CommandSQL.ExecuteReader();    //wpisujemy 36 zebow do DB
                        Reader.Close();
                    }
                    CommandSQL.CommandText = "Select max(id_uzebienie)+1 from lista_zebow";    
                    Reader = CommandSQL.ExecuteReader();
                    Reader.Read();
                    int maxiduzebienie = Reader.GetInt32(0);
                    Reader.Close();
                    for (int i = 0; i < 36; i++)                                //pobieramy max uzebienie i tworzymy kolejne wraz z 36 zebami wpisanymi wyzej
                    {
                        CommandSQL.CommandText = $"INSERT INTO lista_zebow (Id_uzebienie, Id_zeba) VALUES('{maxiduzebienie}', '{zeby[i]}')";
                        Reader = CommandSQL.ExecuteReader();
                        Reader.Close();
                    }
                    CommandSQL.CommandText = qw;
                    Reader = CommandSQL.ExecuteReader();
                    Reader.Close();
                    CommandSQL.CommandText = "Select max(ID_uzytkownika) from uzytkownik";        //Tworzymy uzytkownika
                    Reader = CommandSQL.ExecuteReader();
                    Reader.Read();
                    int maxiduzytkownika = Reader.GetInt32(0);
                    Reader.Close();
                    CommandSQL.CommandText = "Select max(Id_karty) from karta_pacjenta";      
                    Reader = CommandSQL.ExecuteReader();
                    Reader.Read();                  
                    int maxidkarty = Reader.GetInt32(0);
                    Reader.Close();                                             //tworzymy karte pacjenta z odpowiednim id uzytkownika, uzebienia itp.
                    CommandSQL.CommandText = $"INSERT INTO karta_pacjenta (Id_karty, Id_uzebienie, Id_pacjenta, Id_lekarza) VALUES ('{maxidkarty + 1}', '{maxiduzebienie}', '{maxiduzytkownika}', '1');";
                    Reader = CommandSQL.ExecuteReader();
                    MessageBox.Show("Użytkownik został dodany do Bazy danych");
                    Thread.Sleep(3000);
                    Connection.Close();
                    return true;
                   
                }
                
            }


            catch (SqlException)
            {

                MessageBox.Show("Bład połączenia z bazą danych", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Wystąpił nieoczekiwany błąd", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }


    }
}
