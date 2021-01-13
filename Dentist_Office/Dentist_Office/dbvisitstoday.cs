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
    class dbvisitstoday
    {
        public bool dbtry(string qw, object wolneterminy)
        {
            try
            {


                string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=dentysta;";//polaczenie z DB
                MySqlConnection Connection = new MySqlConnection(connection);
                Connection.Open();
                //MySqlCommand CommandSQL = Connection.CreateCommand();
                //CommandSQL.CommandText = qw;//zapytanie do bazy
                //MySqlDataReader Reader = CommandSQL.ExecuteReader();

                MySqlDataAdapter AdapterSQL = new MySqlDataAdapter();
                AdapterSQL.SelectCommand = new MySqlCommand(qw, Connection);
                MySqlCommandBuilder builder = new MySqlCommandBuilder(AdapterSQL);
                DataTable dane = new DataTable();
                AdapterSQL.Fill(dane);
                wolneterminy = dane.DefaultView;
                AdapterSQL.Update(dane);
                return true;



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
