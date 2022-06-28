using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Entities;
using TypicalSchoolWebsite_API.Models.Post;
using TypicalSchoolWebsite_API.Validation.ValidationParams;


namespace TypicalSchoolWebsite_API.Validation.Post
{
    public class CreatePostDTO_Validator : AbstractValidator<CreatePostDTO>
    {
        public CreatePostDTO_Validator()
        {
            //Title
            RuleFor(x => x.Title)
                .MinimumLength(Post_ValidationParams.TitleMinLength)
                .MaximumLength(Post_ValidationParams.TitleMaxLength);

            //TextContent
            RuleFor(x => x.TextContent)
                .MinimumLength(Post_ValidationParams.TextContentMinLength)
                .MaximumLength(Post_ValidationParams.TextContentMaxLength);
        }
    }
}
