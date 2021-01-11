using Dentist_Office.Exceptions;
using Dentist_Office.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;

namespace Dentist_Office.ViewModels
{
	class RegistrationViewModel : BaseModel
	{
       
		private UserModel user = new UserModel();

		public string FirstName {
			get
			{
				return user.FirstName;
			}

			set 
			{
                
                if (String.IsNullOrEmpty(user.FirstName))
				{
                    MessageBox.Show($"{new UserEmptyFirstName()}","Błąd",MessageBoxButton.OK,MessageBoxImage.Information);

				}
				user.FirstName = value;
				OnPropertyChange("FirstName");
				OnPropertyChange("FullName");
			}
		}
		public string LastName
		{
			get
			{
				return user.LastName;
			}

			set
			{
				if (String.IsNullOrEmpty(user.LastName))
				{
					MessageBox.Show($"{new UserEmptyLastName()}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);

				}
				user.LastName = value;
				OnPropertyChange("FirstName");
				OnPropertyChange("FullName");
			}
		}

		public string Pesel 
        { 
            get { return user.PESEL; }
			set
			{
				if (PeselValidator.ValidatePesel(user.PESEL) == false)
				{
                    MessageBox.Show($"{new UserInvalidPESEL()}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    user.PESEL = null;
				} 
            } 
        }
        public void imie(string imie, string nazwisko, string pesel)
        {
            user.FirstName = imie;
            user.LastName = nazwisko;
            user.PESEL = pesel;
        }
      


    }

    /// <summary>
    /// Klasa statyczna PeselValidator.
    /// </summary>
    public static class PeselValidator
    {
        /// <summary>
        /// Mnozniki dla PESEL
        /// </summary>
        private static readonly int[] mnozniki = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };

        /// <summary>
        /// Sprawdza PESEL pod kątem poprawności
        /// </summary>
        /// <param name="pesel">PESEL string</param>
        /// <returns>true = OK; false = NOK</returns>
        public static bool ValidatePesel(string pesel)
        {
            bool toRet = false;
            try
            {
                if (pesel.Length == 11)
                {
                    toRet = CountSum(pesel).Equals(pesel[10].ToString());
                }
            }
            catch (Exception)
            {
                toRet = false;
            }
            return toRet;
        }

        /// <summary>
        /// Liczy sumę cyfr znaczących PESEL
        /// </summary>
        /// <param name="pesel">PESEL string</param>
        /// <returns>SUMA string</returns>
        private static string CountSum(string pesel)
        {
            int sum = 0;
            for (int i = 0; i < mnozniki.Length; i++)
            {
                sum += mnozniki[i] * int.Parse(pesel[i].ToString());
            }

            int reszta = sum % 10;
            return reszta == 0 ? reszta.ToString() : (10 - reszta).ToString();
        }
    }
    
} 

