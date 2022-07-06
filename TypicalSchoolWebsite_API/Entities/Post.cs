using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TypicalSchoolWebsite_API.Entities
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string TextContent { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastEditDate { get; set; }

        public bool IsActive { get; set; }


        //Foreign
        //User
        [ForeignKey("User")]
        public int UserId { get; set; }
        
        public virtual User User { get; set; }


        //PostCategory
        [ForeignKey("PostCategory")]
        public int? PostCategoryId { get; set; }

        public virtual PostCategory PostCategory { get; set; }


        //ImageFile
        [ForeignKey("ImageFile")]
        public int ImageFileId { get; set; }

        public virtual ImageFile ImageFile { get; set; }
    }
}
