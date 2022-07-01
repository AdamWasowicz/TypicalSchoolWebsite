using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace TypicalSchoolWebsite_API.Entities
{
    public class PostLog
    {
        [Key]
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


        //Foreign
        //User
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }


        //Post
        [ForeignKey("Post")]
        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
