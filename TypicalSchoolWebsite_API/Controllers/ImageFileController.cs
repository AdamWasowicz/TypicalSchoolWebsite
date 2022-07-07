using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
using TypicalSchoolWebsite_API.Entities;
using TypicalSchoolWebsite_API.Interfaces;
using TypicalSchoolWebsite_API.Models.Post;


namespace TypicalSchoolWebsite_API.Controllers
{
    [ApiController]
    [Route("imageFile")]
    public class ImageFileController : ControllerBase
    {
        private readonly IImageFileService _imageFileService;
        private readonly HttpClient _httpClient;

        public ImageFileController(
            IImageFileService imageFileService,
            HttpClient httpClient)
        {
            _imageFileService = imageFileService;
            _httpClient = httpClient;
        }


        [HttpGet("testConnectionToService")]
        public async Task<ActionResult> TestConnection()
        {
            var result = await _imageFileService.CheckConnection();

            if (result != 0)
                return new StatusCodeResult(500);

            return new StatusCodeResult(200);
        }

        [HttpGet("getImage/{storageName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetImage([FromRoute] string storageName)
        {
            var result = await _imageFileService.GetImage(storageName);
    

            var contentProvider = new FileExtensionContentTypeProvider();
            contentProvider.TryGetContentType(result.Item2, out string type);

            return File(result.Item1, type, result.Item2);
        }
    }
}
