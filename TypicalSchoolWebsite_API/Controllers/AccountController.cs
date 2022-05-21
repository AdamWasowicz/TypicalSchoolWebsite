using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Entities;
using TypicalSchoolWebsite_API.Interfaces;
using TypicalSchoolWebsite_API.Models.Account;

namespace TypicalSchoolWebsite_API.Controllers
{
    [ApiController]
    [Route("Account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;


        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDTO dto)
        {
            int resultCode = _accountService.RegisterUser(dto);

            if (resultCode == 0)
                return Ok();
            else
                return BadRequest();

        }


        [HttpPost("login")]
        public ActionResult LogIn([FromBody] LogInDTO dto)
        {
            var logInResult = _accountService.LogIn(dto);

            if (logInResult.Item1 == 0)
                return Ok(logInResult.Item2);
            else
                return BadRequest();
        }
    }
}
