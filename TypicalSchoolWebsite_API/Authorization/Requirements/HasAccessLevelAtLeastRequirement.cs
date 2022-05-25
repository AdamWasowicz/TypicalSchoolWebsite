using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypicalSchoolWebsite_API.Authorization.Requirements
{
    public class HasAccessLevelAtLeastRequirement : IAuthorizationRequirement
    {
        public int MinimumAccessLevel { get; }


        public HasAccessLevelAtLeastRequirement(int minimumAccessLevel)
        {
            MinimumAccessLevel = minimumAccessLevel;
        }
    }
}
