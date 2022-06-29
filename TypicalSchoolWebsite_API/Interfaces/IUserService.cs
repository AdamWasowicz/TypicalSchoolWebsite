using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Models.User;

namespace TypicalSchoolWebsite_API.Interfaces
{
    public interface IUserService
    {
        public List<UserDTO> GetAllUsers();

        public UserDTO GetUserById(int id);

        public int DeleteUserById(int id, ClaimsPrincipal userClaims);

        public UserDTO EditUserById(EditUserDTO dto, ClaimsPrincipal userClaims);
    }
}
