using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Authorization.Requirements;
using TypicalSchoolWebsite_API.Entities;

namespace TypicalSchoolWebsite_API.Authorization.Handlers
{
    public class RoleResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Role>
    {
        private readonly TSW_DbContext _dbContext;


        public RoleResourceOperationRequirementHandler(TSW_DbContext dbContext)
        {
            _dbContext = dbContext;
        }


        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ResourceOperationRequirement requirement,
            Role resource)
        {
            //No claims
            if (context.User.Claims.Count() == 0)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            //User access level
            var claimUserId = Convert.ToInt32(context.User.FindFirst(c => c.Type == "UserId").Value);
            var claimUserRoleId = Convert.ToInt32(context.User.FindFirst(c => c.Type == "RoleId").Value);
            var userAccessLevel = _dbContext.Roles
                .Where(r => r.Id == claimUserRoleId)
                    .FirstOrDefault()
                        .AccessLevel;


            //DELETE
            if (requirement.ResourceOperation == ResourceOperation.Delete)
            {
                //Admin
                var accessLevel = _dbContext.Roles
                    .Where(r => r.RoleName == "Admin")
                        .FirstOrDefault()
                            .AccessLevel;

                if (claimUserRoleId >= accessLevel)
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }

                context.Fail();
                return Task.CompletedTask;
                
            }


            //CREATE
            if (requirement.ResourceOperation == ResourceOperation.Create)
            {
                //User and up
                var userExists = _dbContext.Users
                    .Where(u => u.Id == claimUserId)
                        .Any();

                if (userExists)
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }


            //Update
            if (requirement.ResourceOperation == ResourceOperation.Update)
            {
                //Admin
                var accessLevel = _dbContext.Roles
                    .Where(r => r.RoleName == "Admin")
                        .FirstOrDefault()
                            .AccessLevel;

                if (claimUserRoleId >= accessLevel)
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }

                context.Fail();
                return Task.CompletedTask;
            }


            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
