using Microsoft.AspNetCore.Http;


namespace TypicalSchoolWebsite_API.Models.ImageFile
{
    public class CreateImageFileDTO
    {
        public IFormFile File { get; set; }
    }
}
