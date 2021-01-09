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
       
       

        public void dbtry(string qw)
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
                        if(Reader ["status"].ToString()=="1")
                        {
                            patient okno = new patient();
                            okno.Show();
                            Login test = new Login();
                            test.Close();
                        }
                        else if(Reader["status"].ToString() == "2")
                        {
                            secretary okno = new secretary();
                            okno.Show();
                        }
                        else if (Reader["status"].ToString() == "3")
                        {
                            doctor okno = new doctor();
                            okno.Show();
                        }
                        else
                        {
                            MessageBox.Show("zle");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Błędny login lub hasło");
                }
                Reader.Close();
                Connection.Close();                
            }

  catch (SqlException e)
{
    Console.WriteLine("Wystąpił nieoczekiwany błąd!");
    Console.WriteLine(e.Message);
    Console.ReadKey();
}
}
    }
}
