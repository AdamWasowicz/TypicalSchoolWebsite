using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypicalSchoolWebsite_API.Exceptions
{
    public class BadAuthorizationExeption : Exception
    {
        public BadAuthorizationExeption(string message) : base(message)
        {

        }
    }
}
