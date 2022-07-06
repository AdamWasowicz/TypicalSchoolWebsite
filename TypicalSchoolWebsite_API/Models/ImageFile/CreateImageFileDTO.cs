using Microsoft.AspNetCore.Http;


namespace TypicalSchoolWebsite_API.Models.ImageFile
{
    public class CreateImageFileDTO
    {
        public IFormFile File { get; set; }

        public string FileName { get; set; }

        public string FileExtenstion { get; set; }
    }
}
