using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Entities;

namespace TypicalSchoolWebsite_API.Other
{

    public class Seeder
    {
        private readonly TSW_DbContext _dbContext;


        public Seeder(TSW_DbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void SeedRoles()
        {
            if (_dbContext.Database.CanConnect() & !_dbContext.Roles.Any())
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
    }
}
