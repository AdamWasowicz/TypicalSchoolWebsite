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


        //FirstName
        public const int FirstNameMinLength = 2;
        public const int FirstNameMaxLength = 32;


        //SecondName
        public const int SecondNameMinLength = 0;
        public const int SecondNameMaxLength = 32;


        //Surname
        public const int SurnameMinLength = 2;
        public const int SurnameMaxLength = 32;


        //Gender
        public static readonly List<char> GenderArray = new List<char> { 'F', 'M' };
    }
}
