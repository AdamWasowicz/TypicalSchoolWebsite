using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Entities;
using TypicalSchoolWebsite_API.Models.Post;
using TypicalSchoolWebsite_API.Models.User;
using TypicalSchoolWebsite_API.Validation.ValidationParams;


namespace TypicalSchoolWebsite_API.Validation.User
{
    public class EditUserDTO_Validator : AbstractValidator<EditUserDTO>
    {
        public EditUserDTO_Validator()
        {
            //UserName
            RuleFor(x => x.UserName)
                .MinimumLength(User_ValidationParams.UserNameMinLength)
                .MaximumLength(User_ValidationParams.UserNameMaxLength);


            //FirstName
            RuleFor(x => x.FirstName)
                .MinimumLength(User_ValidationParams.FirstNameMinLength)
                .MaximumLength(User_ValidationParams.FirstNameMaxLength);


            //SecondName
            RuleFor(x => x.SecondName)
                .MinimumLength(User_ValidationParams.SecondNameMinLength)
                .MaximumLength(User_ValidationParams.SecondNameMaxLength);


            //Surname
            RuleFor(x => x.Surname)
                .MinimumLength(User_ValidationParams.SurnameMinLength)
                .MaximumLength(User_ValidationParams.SurnameMaxLength);


            //Gender
            RuleFor(x => x.Gender)
                .Custom((g, context) =>
                {
                    if (!(User_ValidationParams.GenderArray.Contains((char)g)))
                        context.AddFailure("Invalid gender");
                });
        }
    }
}
