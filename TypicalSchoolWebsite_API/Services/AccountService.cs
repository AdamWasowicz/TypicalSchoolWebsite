using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Entities;
using TypicalSchoolWebsite_API.Interfaces;
using TypicalSchoolWebsite_API.Models.Account;
using TypicalSchoolWebsite_API.Other;

namespace TypicalSchoolWebsite_API.Services
{
    public class AccountService : IAccountService
    {
        private readonly TSW_DbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authSettings;


        public AccountService(
            TSW_DbContext dbContext,
            IPasswordHasher<User> passwordHasher,
            AuthenticationSettings authSettings
            )
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authSettings = authSettings;
        }


        public string GenerateJwt(User user)
        {
            var claimsObject = new List<Claim>()
            {
                new Claim("UserName", user.UserName),
                new Claim("FirstName", user.FirstName),
                new Claim("Surname", user.Surname),
                new Claim("RoleId", user.RoleId.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expireDate = DateTime.Now.AddHours(Convert.ToDouble(_authSettings.JwtExpireTimeHours));

            var token = new JwtSecurityToken(
                _authSettings.JwtIssuer,
                claims: claimsObject,
                expires: expireDate,
                signingCredentials: cred
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }


        public int RegisterUser(RegisterUserDTO dto)
        {
            var timestamp = DateTime.Now;
            var basicRole = _dbContext.Roles
                .Where(r => r.RoleName == "Writer")
                .FirstOrDefault();


            var newUser = new User()
            {
                Email = dto.Email,
                UserName = dto.UserName,

                FirstName = dto.FirstName,
                SecondName = dto.SecondName,
                Surname = dto.Surname,

                RegisterDate = timestamp,
                LastAccountModificationDate = timestamp,

                IsActive = true,
                IsSuspended = false,

                RoleId = basicRole.Id,
            };

            var passwordHash = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = passwordHash;

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            return 0;
        }


        public Tuple<int, string> LogIn(LogInDTO dto)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UserName == dto.UserName);

            //if (user is null)
            //    throw new BadRequestException("Invalid username or password");

            var passwordCheckResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (passwordCheckResult == PasswordVerificationResult.Failed)
            {
                //    throw new BadRequestException("Invalid username or password");
            }

            string JwtToken = GenerateJwt(user);

            var returnValue = new Tuple<int, string>(0, JwtToken);

            return returnValue;
        }
    }
}
