using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypicalSchoolWebsite_API.Models.Post
{
    public class EditPostDTO
    {
        public int Id { get; set; }

        public string AccessName { get; set; }

        public string Title { get; set; }

        public string TextContent { get; set; }

        
        public int PostCategoryId { get; set; }
    }
}
