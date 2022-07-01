using System.ComponentModel.DataAnnotations.Schema;
using System;
using TypicalSchoolWebsite_API.Models.User;
using TypicalSchoolWebsite_API.Models.Post;


namespace TypicalSchoolWebsite_API.Models.PostLog
{
    public class PostLogDTO
    {
        public int Id { get; set; }

        public DateTime When { get; set; }

        public string Operation { get; set; }


        //Changes
        //Title
        public string PreviousTitle { get; set; }
        public string CurrentTitle { get; set; }

        //TextContent
        public string PreviousTextContent { get; set; }
        public string CurrentTextContent { get; set; }

        //IsActive
        public bool PreviousIsActive { get; set; }
        public bool CurrentIsActive { get; set; }


        public UserDTO User { get; set; }

        public PostDTO Post { get; set; }
    }
}
