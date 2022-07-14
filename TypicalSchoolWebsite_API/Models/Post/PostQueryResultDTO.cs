using System.Collections.Generic;

namespace TypicalSchoolWebsite_API.Models.Post
{
    public class PostQueryResultDTO
    {
        public int DesiredPage { get; set; }

        public int DesiredNumberOfItems { get; set; }

        public int MaxPages { get; set; }

        public List<PostDTO> Posts { get; set; }
    }
}
