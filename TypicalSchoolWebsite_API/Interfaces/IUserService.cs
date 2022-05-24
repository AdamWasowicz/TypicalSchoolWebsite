using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Models.User;

namespace TypicalSchoolWebsite_API.Interfaces
{
    public interface IUserService
    {
        public List<UserDTO> GetAllUsers();
    }
}
