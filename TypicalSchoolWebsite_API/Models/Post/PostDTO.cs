using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Models.User;

namespace TypicalSchoolWebsite_API.Models.Post
{
    public class PostDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string TextContent { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastEditDate { get; set; }

        public UserDTO User { get; set; }

        public bool IsActive { get; set; }
    }
}
