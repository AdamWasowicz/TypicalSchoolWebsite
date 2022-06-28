using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Entities;
using TypicalSchoolWebsite_API.Models.Post;
using TypicalSchoolWebsite_API.Models.Role;
using TypicalSchoolWebsite_API.Validation.ValidationParams;


namespace TypicalSchoolWebsite_API.Validation.Role
{
    public class EditRoleDTO_Validator : AbstractValidator<EditRoleDTO>
    {
        public EditRoleDTO_Validator()
        {
            //RoleName
            RuleFor(x => x.RoleName)
                .MinimumLength(Role_ValidationParams.RoleNameMinLength)
                .MaximumLength(Role_ValidationParams.RoleNameMaxLength);

            //Description
            RuleFor(x => x.Description)
                .MinimumLength(Role_ValidationParams.DescriptionMinLength)
                .MaximumLength(Role_ValidationParams.DescriptionMaxLength);
        }
    }
}
