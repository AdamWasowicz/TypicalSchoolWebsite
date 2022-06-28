using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypicalSchoolWebsite_API.Validation.ValidationParams
{
    static public class User_ValidationParams
    {
        //Password
        public const int PasswordMinLength = 8;
        public const int PasswordMaxLength = 32;


        //UserName
        public const int UserNameMinLength = 8;
        public const int UserNameMaxLength = 32;
    }
}
