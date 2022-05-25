using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Authorization.Requirements;
using TypicalSchoolWebsite_API.Entities;
using TypicalSchoolWebsite_API.Exceptions;


namespace TypicalSchoolWebsite_API.Authorization.Handlers
{
    public class HasAccessLevelAtLeastRequirementHandler : AuthorizationHandler<HasAccessLevelAtLeastRequirement>
    {
        private readonly TSW_DbContext _dbContext;


        public HasAccessLevelAtLeastRequirementHandler(TSW_DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasAccessLevelAtLeastRequirement requirement)
        {
            if (context.User.Claims.Count() == 0)
                throw new BadRequestException("Missing authentication");

            var claim_RoleId = Convert.ToInt32(context.User.FindFirst(c => c.Type == "RoleId").Value);
            var role = _dbContext.Roles.FirstOrDefault(r => r.Id == claim_RoleId);

            if (role == null)
                throw new BadRequestException("Invalid role");

            if (role.AccessLevel >= requirement.MinimumAccessLevel)
                context.Succeed(requirement);
            else
                context.Fail();
            

            return Task.CompletedTask;
        }
    }
}
