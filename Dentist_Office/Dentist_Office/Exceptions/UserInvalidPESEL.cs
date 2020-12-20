using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentist_Office.Exceptions
{
	public class UserInvalidPESEL : Exception
	{
		public UserInvalidPESEL() : base($"Podany PESEL jest nieprawidłowy")
		{

		}
	}
}
