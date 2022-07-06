using Microsoft.AspNetCore.Http;
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
        public ActionResult<string> TestFileUpload([FromForm] IFormFile file)
        {
            return Ok(Request.Form.Files[0].FileName);
        }

        [HttpGet("testConnection")]
        public ActionResult TestConnection()
        {
            return Ok("works...");
        }
    }
}
