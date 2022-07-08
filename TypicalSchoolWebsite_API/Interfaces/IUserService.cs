using System.Collections.Generic;
using System.Security.Claims;
using TypicalSchoolWebsite_API.Models.User;

namespace TypicalSchoolWebsite_API.Interfaces
{
    public interface IUserService
    {
        int DeleteUserById(int id, ClaimsPrincipal userClaims);
        int DeleteUserByUserName(string userName, ClaimsPrincipal userClaims);
        UserDTO EditUserById(EditUserDTO dto, ClaimsPrincipal userClaims);
        UserDTO EditUserByUserName(EditUserDTO dto, ClaimsPrincipal userClaims);
        List<UserDTO> GetAllUsers();
        UserDTO GetUserById(int id);
        UserDTO GetUserByUserName(string userName);
    }
}