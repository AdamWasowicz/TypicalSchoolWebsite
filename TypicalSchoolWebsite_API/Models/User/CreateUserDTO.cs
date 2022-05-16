using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypicalSchoolWebsite_API.Models.User
{
    public class CreateUserDTO
    {
        //Account
        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string PasswordRepeat { get; set; }


        //Personal
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Surname { get; set; }
    }
}
