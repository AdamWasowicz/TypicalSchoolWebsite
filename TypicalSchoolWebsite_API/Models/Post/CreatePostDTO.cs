using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TypicalSchoolWebsite_API.Models.Post
{
    public class CreatePostDTO
    {
        public  string Title { get; set; }

        public string TextContent { get; set; }

        public IFormFile PostImage { get; set; }
    }
}
