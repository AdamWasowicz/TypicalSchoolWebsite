using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Entities;
using TypicalSchoolWebsite_API.Models.Post;
using TypicalSchoolWebsite_API.Validation.ValidationParams;

namespace TypicalSchoolWebsite_API.Validation.Post
{
    public class EditPostDTO_Validator : AbstractValidator<EditPostDTO>
    {
        public EditPostDTO_Validator(TSW_DbContext dbContext)
        {
            //Title
            RuleFor(x => x.Title)
                .MinimumLength(Post_ValidationParams.TitleMinLength)
                .MaximumLength(Post_ValidationParams.TitleMaxLength)
                .Custom((value, context) =>
{
                    var isTitleInUse = dbContext.Posts.Any(p => p.Title == value);

                    if (isTitleInUse)
                        context.AddFailure("Title", "Already Exists");
                });

            //TextContent
            RuleFor(x => x.TextContent)
                .MinimumLength(Post_ValidationParams.TextContentMinLength)
                .MaximumLength(Post_ValidationParams.TextContentMaxLength);
        }
    }
}
