using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypicalSchoolWebsite_API.Validation.ValidationParams
{
    public class Post_ValidationParams
    {
        //Title
        public const int TitleMinLength = 3;
        public const int TitleMaxLength = 32;

        //TextContent
        public const int TextContentMinLength = 8;
        public const int TextContentMaxLength = 256;
    }
}
