using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Entities;
using TypicalSchoolWebsite_API.Interfaces;
using TypicalSchoolWebsite_API.Models.Account;

namespace TypicalSchoolWebsite_API.Services
{
    public class AccountService : IAccountService
    {
        private readonly TSW_DbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;


        public AccountService(
            TSW_DbContext dbContext,
            IPasswordHasher<User> passwordHasher
            )
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
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
    }
}
