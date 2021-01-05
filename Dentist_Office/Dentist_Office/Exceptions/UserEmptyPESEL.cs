using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentist_Office.Exceptions
{
    class UserEmptyPESEL : Exception
    {
        public UserEmptyPESEL() : base(" Pole pesel nie może być puste")
        {

        }
    }
}
