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
using Dentist_Office.Exceptions;
using Dentist_Office.Models;
using Dentist_Office.ViewModels;

namespace Dentist_Office
{
    /// <summary>
    /// Logika interakcji dla klasy Registern.xaml
    /// </summary>
    public partial class Registern : Window
    {
        public Registern()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            RegistrationViewModel reg = new RegistrationViewModel();
            reg.imie(TextBoxName.Text, TextBoxLastName.Text, TextBoxIdNumber.Text);
            reg.FirstName = TextBoxName.Text;
            reg.LastName = TextBoxLastName.Text;
            reg.Pesel = TextBoxIdNumber.Text;
            
            
            string query = $"INSERT INTO uzytkownik (ID_uzytkownika, Imie, Nazwisko, PESEL, Haslo, status) VALUES(NULL, '{reg.FirstName}', '{reg.LastName}', '{reg.Pesel}', '{TextBoxPassword.Password}', '1');";
            dbregister test = new dbregister();

           
            if (reg.Pesel != null)
            {
               if(test.dbtry(query, reg.Pesel)==true)
                {
                    Close();
                }
               
               
            }
            else
            {
                
            }


        }
    }
}
