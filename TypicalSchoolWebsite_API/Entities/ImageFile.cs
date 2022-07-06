using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TypicalSchoolWebsite_API.Entities
{
    public class ImageFile
    {
        [Key]
        public int Id { get; set; }

        public string UserGivenName { get; set; }

        public string StorageName { get; set; }

        public string FileFormat { get; set; }

        public int FileSizeInKB { get; set; }

        public DateTime WhenCreated { get; set; }



        //Foreign
        //User
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
