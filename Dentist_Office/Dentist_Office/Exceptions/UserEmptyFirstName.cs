using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentist_Office.Exceptions
{
	class UserEmptyFirstName : Exception
	{
		public UserEmptyFirstName() : base("Imię nie może być puste")
		{

		}
	}
}
