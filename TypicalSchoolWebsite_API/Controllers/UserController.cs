using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "IsModerator")]
        public ActionResult<List<UserDTO>> GetAllUsers()
        {
            var usersDTO = _userService.GetAllUsers();

            return Ok(usersDTO);
        }
    }
}
