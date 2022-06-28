using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Entities;
using TypicalSchoolWebsite_API.Interfaces;
using TypicalSchoolWebsite_API.Models.Role;
using TypicalSchoolWebsite_API.Models.User;

namespace TypicalSchoolWebsite_API.Controllers
{
    [ApiController]
    [Route("role")]
    public class RoleController: ControllerBase
    {
        private readonly IRoleService _roleService;


        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        [HttpPost("createRole")]
        [Authorize(Policy = "IsAdmin")]
        public ActionResult CreateRole([FromBody] CreateRoleDTO dto)
        {
            var operationResult = _roleService.CreateRole(dto, User);

            if (operationResult != 0)
                return StatusCode(500);

            return Created(operationResult.ToString(), null);
        }


        [HttpGet("getAllRoles")]
        [Authorize(Policy = "IsAdmin")]
        public ActionResult<List<RoleDTO>> GetAllRoles()
        {
            var operationResult = _roleService.GetAllRoles();

            return Ok(operationResult);
        }


        [HttpGet("getRoleById/{id}")]
        [Authorize(Policy = "IsAdmin")]
        public ActionResult<RoleDTO> GetRoleById([FromRoute] int id)
        {
            var roleDTO = _roleService.GetRoleById(id);

            return Ok(roleDTO);
        }


        [HttpDelete("deleteRoleById/{id}")]
        [Authorize(Policy = "IsAdmin")]
        public ActionResult DeleteRoleById([FromRoute] int id)
        {
            var result = _roleService.DeleteRoleById(id, User);

            if (result != 0)
                return StatusCode(500);

            return NoContent();
        }


        [HttpPut("editRoleById")]
        [Authorize(Policy = "IsAdmin")]
        public ActionResult<RoleDTO> EditRoleById([FromBody] EditRoleDTO dto)
        {
            var roleDTO = _roleService.EditRoleById(dto, User);

            return Ok(roleDTO);
        }
    }
}
