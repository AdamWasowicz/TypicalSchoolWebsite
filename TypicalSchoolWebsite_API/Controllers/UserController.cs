﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Entities;
using TypicalSchoolWebsite_API.Interfaces;
using TypicalSchoolWebsite_API.Models.User;

namespace TypicalSchoolWebsite_API.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        //For testing only
        [HttpGet("getAllUsers")]
        [Authorize(Policy = "IsAdmin")]
        public ActionResult<List<UserDTO>> GetAllUsers()
        {
            var usersDTO = _userService.GetAllUsers();

            return Ok(usersDTO);
        }


        [HttpGet("getUserById/{id}")]
        public ActionResult<UserDTO> GetUserById([FromRoute] int id)
        {
            var userDTO = _userService.GetUserById(id);

            return Ok(userDTO);
        }


        [HttpDelete("deleteUserById/{id}")]
        [Authorize(Policy = "IsAdmin")]
        public ActionResult DeleteUserById([FromRoute] int id)
        {
            var result = _userService.DeleteUserById(id, User);

            if (result != 0)
                return StatusCode(500);

            return NoContent();
        }


        [HttpPut("editUser")]
        [Authorize(Policy = "IsWriter")]
        public ActionResult<UserDTO> EditUser([FromBody] EditUserDTO dto)
        {
            var userDTO = _userService.EditUserById(dto, User);

            return Ok(userDTO);
        }
    }
}
