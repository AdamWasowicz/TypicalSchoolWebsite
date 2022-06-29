using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Authorization.Requirements;
using TypicalSchoolWebsite_API.Entities;

namespace TypicalSchoolWebsite_API.Authorization.Handlers
{
    public class UserResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, User>
    {
        private readonly TSW_DbContext _dbContext;


        public UserResourceOperationRequirementHandler(TSW_DbContext dbContext)
        {
            _dbContext = dbContext;
        }


        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ResourceOperationRequirement requirement,
            User resource)
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


                //User
                //User
                if (resource.Id == claimUserId)
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
                else
                {
                    context.Fail();

                }
            }


            //UPDATE
            if (requirement.ResourceOperation == ResourceOperation.Update)
            {
                //Admin and Moderator
                var accessLevel = _dbContext.Roles
                    .Where(r => r.RoleName == "Admin")
                        .FirstOrDefault()
                            .AccessLevel;

                if (claimUserRoleId >= accessLevel)
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }


                if (resource.Id == claimUserId)
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
                else
                {
                    context.Fail();
                    return Task.CompletedTask;
                }
            }


            context.Fail();
            return Task.CompletedTask;
        }
    }
}
