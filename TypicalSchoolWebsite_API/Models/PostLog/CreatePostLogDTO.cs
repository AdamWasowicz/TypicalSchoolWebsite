using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Models.Post;

namespace TypicalSchoolWebsite_API.Models.PostLog
{
    public class CreatePostLogDTO
    {
        public int UserId { get; set; }

        public string Operation { get; set; }

        public PostDTO PreviousState { get; set; }

        public PostDTO CurrentState { get; set; }
    }
}
