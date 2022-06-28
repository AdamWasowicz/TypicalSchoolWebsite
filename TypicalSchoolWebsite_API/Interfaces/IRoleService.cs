using System.Collections.Generic;
using System.Security.Claims;
using TypicalSchoolWebsite_API.Models.Role;

namespace TypicalSchoolWebsite_API.Interfaces
{
    public interface IRoleService
    {
        int CreateRole(CreateRoleDTO dto, ClaimsPrincipal userClaims);
        int DeleteRoleById(int id, ClaimsPrincipal userClaims);
        RoleDTO EditRoleById(EditRoleDTO dto, ClaimsPrincipal userClaims);
        List<RoleDTO> GetAllRoles();
        RoleDTO GetRoleById(int id);
    }
}