using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Models.ImageFile;
using TypicalSchoolWebsite_FileStorageService.Interfaces;

namespace TypicalSchoolWebsite_FileStorageService.Controllers
{
    [ApiController]
    [Route("fileStorage")]
    public class FileStorageController : ControllerBase
    {
        private readonly IFileStorageService _fileStorageService;


        public FileStorageController(
            IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }


        [HttpPost("createImageFile")]
        public ActionResult<string> CreateImage([FromForm] IFormFile file)
        {
            var result = _fileStorageService.CreateImage(file);

            if (result != "")
                return Ok(result);

            return StatusCode(502);
        }


        [HttpGet("getImageFile/{hashedName}")]
        public ActionResult ReturnImage([FromRoute] string hashedName)
        {
            var result = _fileStorageService.ReturnImage(hashedName);

            if (result.Item2 == "error")
                return StatusCode(501);

            var file = result.Item1;
            var name = result.Item2.Split('.')[0];

            var contentProvider = new FileExtensionContentTypeProvider();
            contentProvider.TryGetContentType(result.Item2, out string type);

            return File(file, type, name);
        }


        [HttpDelete("deleteImageFile/{hashedName}")]
        public ActionResult<int> DeleteImage([FromRoute] string hashedName)
        {
            var result = _fileStorageService.DeleteImage(hashedName);

            if (result == 0)
                return Ok();

            if (result == -1)
                return StatusCode(404);
            else
                return StatusCode(500);
        }


        [HttpGet("testConnection")]
        public ActionResult TestConnection()
        {
            return Ok("works...");
        }
    }
}
