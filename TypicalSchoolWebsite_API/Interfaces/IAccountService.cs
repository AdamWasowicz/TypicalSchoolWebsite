using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Models.Account;

namespace TypicalSchoolWebsite_API.Interfaces
{
    public interface IAccountService
    {
        int RegisterUser(RegisterUserDTO dto);
    }
}
