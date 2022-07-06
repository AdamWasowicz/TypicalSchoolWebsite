using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Models.ImageFile;

namespace TypicalSchoolWebsite_FileStorageService.Controllers
{
    [ApiController]
    [Route("fileStorage")]
    public class FileStorageController : ControllerBase
    {
        public FileStorageController()
        {
            
        }

        [HttpPost("createImageFile")]
        public ActionResult<string> TestFileUpload([FromBody] CreateImageFileDTO dto)
        {
            return Ok("a");
        }

        [HttpGet("testConnection")]
        public ActionResult TestConnection()
        {
            return Ok("works...");
        }
    }
}
