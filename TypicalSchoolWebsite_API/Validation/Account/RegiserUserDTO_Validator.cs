using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Entities;
using TypicalSchoolWebsite_API.Models.Account;
using TypicalSchoolWebsite_API.Validation.ValidationParams;

namespace TypicalSchoolWebsite_API.Validation.Account
{
    public class RegiserUserDTO_Validator : AbstractValidator<RegisterUserDTO>
    {
        private readonly User_ValidationParams _params;

        public RegiserUserDTO_Validator(TSW_DbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();


            RuleFor(x => x.Password)
                .Equal(y => y.PasswordRepeat);


            RuleFor(x => x.Password)
                .MinimumLength(_params.PasswordMinLenght)
                .MaximumLength(_params.PasswordMaxLength);


            RuleFor(x => x.UserName)
                .Custom((value, context) =>
                {
                    var isUserNameInUse = dbContext.Users.Any(u => u.UserName == value);

                    if (isUserNameInUse)
                        context.AddFailure("UserName", "Taken");
                });


            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var isEmailInUse = dbContext.Users.Any(u => u.Email == value);

                    if (isEmailInUse)
                        context.AddFailure("Email", "Taken");
                });
        }
    }
}
