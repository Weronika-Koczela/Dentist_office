using Dentist_Office.Exceptions;
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
    /// Logika interakcji dla klasy Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(TextBoxLogin.Text.Length<1)
            {
                MessageBox.Show($"{new UserEmptyPESEL()}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if(TextBoxPassword.Password.Length<1)
                {
                    MessageBox.Show($"{new UserEmptyPassword()}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Trwa logowanie");
                }
            }
            //pozniej sie tu dopisze lub do tego utworzy sie klase ale trzeba najpierw podlaczyc z baza danych.
        }
    }
}
