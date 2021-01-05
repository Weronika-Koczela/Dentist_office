using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentist_Office.Exceptions
{
    class UserEmptyPassword : Exception
    {
        public UserEmptyPassword() : base("Hasło nie może być puste")
        {

        }
    }
}
