using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Entities;
using TypicalSchoolWebsite_API.Interfaces;
using TypicalSchoolWebsite_API.Models.Post;


namespace TypicalSchoolWebsite_API.Controllers
{
    [ApiController]
    [Route("imageFile")]
    public class ImageFileController
    {
        private readonly IImageFileService _imageFileService;


        public ImageFileController(
            IImageFileService imageFileService)
        {
            _imageFileService = imageFileService;
        }


        [HttpGet]
        public async Task<ActionResult> TestConnection()
        {
            var result = await _imageFileService.CheckConnection();

            if (result != 0)
                return new StatusCodeResult(500);

            return new StatusCodeResult(200);
        }
    }
}
