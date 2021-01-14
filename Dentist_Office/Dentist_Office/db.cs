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

namespace Dentist_Office
{
    public class db
    {
        public bool dbtry(string qw, string pesel)
        {
            try
            {
                string conqw = "datasource=127.0.0.1;port=3306;username=root;password=;database=dentysta;";
                MySqlConnection Connection = new MySqlConnection(conqw);
                Connection.Open();
                MySqlCommand CommandSQL = Connection.CreateCommand();
                CommandSQL.CommandText = qw;
                MySqlDataReader Reader = CommandSQL.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        if (Reader["status"].ToString() == "1")
                        {
                            patient okno = new patient(pesel);
                            okno.Show();
                            //Reader.Close();
                            //Connection.Close();
                            //return true;
                            //Login test = new Login();
                            //test.Close();
                        }
                        else if (Reader["status"].ToString() == "2")
                        {
                            
                            secretary okno = new secretary();
                            //Reader.Close();
                            //Connection.Close();
                            okno.Show();
                           // return true;
                        }
                        else if (Reader["status"].ToString() == "3")
                        {
                            //Reader.Close();
                            //Connection.Close();
                            
                            DentistWindow okno = new DentistWindow();
                            okno.Show();
                            //return true;
                        }
                        else
                        {

                        }
                        //Reader.Close();
                        //Connection.Close();
                    }
                    
                    return true;

                }
                else
                {
                    Reader.Close();
                    Connection.Close();
                    MessageBox.Show("Błędny login lub hasło");
                    return false;
                }
                
            }

            catch (SqlException e)
            {
                
                MessageBox.Show("Wystąpił nieoczekiwany błąd!");
                MessageBox.Show(e.Message);
                return false;
            }
            
        }
    }
}
