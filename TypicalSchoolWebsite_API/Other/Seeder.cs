using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Entities;
using TypicalSchoolWebsite_API.Exceptions;
using TypicalSchoolWebsite_API.Interfaces;
using TypicalSchoolWebsite_API.Models.Account;

namespace TypicalSchoolWebsite_API.Other
{

    public class Seeder
    {
        private readonly TSW_DbContext _dbContext;
        private readonly IAccountService _accountService;


        public Seeder(TSW_DbContext dbContext, IAccountService accountService)
        {
            _dbContext = dbContext;
            _accountService = accountService;
        }


        public void Seed()
        {
            SeedRoles();
            SeedPostCategories();
            SeedAdminUser();
        }


        public void SeedRoles()
        {
            if (_dbContext.Database.CanConnect() && !_dbContext.Roles.Any())
            {
                var roles = new List<Role>()
                {
                    new Role()
                    {
                        RoleName = "Writer",
                        Description = "Writes articles",
                        AccessLevel = 4
                    },

                    new Role()
                    {
                        RoleName = "Moderator",
                        Description = "Can do the same things as Writer and more",
                        AccessLevel = 8
                    },

                    new Role()
                    {
                        RoleName = "Admin",
                        Description = "Can do everything",
                        AccessLevel = 12
                    },
                };


                _dbContext.Roles.AddRange(roles);
                _dbContext.SaveChanges();
            }
        }


        public void SeedPostCategories()
        {
            if (_dbContext.Database.CanConnect() && !_dbContext.PostCategories.Any())
            {
                var basePostCategory = new PostCategory()
                {
                    Name = "None",
                    Description = "Default Post Category"
                };

                _dbContext.PostCategories.Add(basePostCategory);
                _dbContext.SaveChanges();
            }
        }


        public void SeedAdminUser()
        {
            if (_dbContext.Database.CanConnect() && !_dbContext.Users.Any())
            {
                var registerUserDTO = new RegisterUserDTO()
                {
                    Email = "TSW_admin@tsw.net",
                    UserName = "SuperAdmin",
                    Password = "SuperAdminPassword0#",
                    PasswordRepeat = "SuperAdminPassword0#"
                };

                _accountService.RegisterUser(registerUserDTO);


                var adminUser = _dbContext.Users
                    .Where(u => u.UserName == "SuperAdmin")
                        .FirstOrDefault();


                if (adminUser == null)
                    throw new NotFoundException();


                var adminRole = _dbContext.Roles
                    .Where(r => r.RoleName == "Admin")
                        .FirstOrDefault();

                if (adminRole == null)
                    throw new NotFoundException();



                adminUser.RoleId = adminRole.Id;
                _dbContext.SaveChanges();
            }
        }
    }
}
