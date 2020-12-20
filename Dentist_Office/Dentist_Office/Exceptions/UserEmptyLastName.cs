using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentist_Office.Exceptions
{
	class UserEmptyLastName : Exception
	{
		public UserEmptyLastName() : base("Nazwisko nie może być puste") 
		{

		}
	}
}
