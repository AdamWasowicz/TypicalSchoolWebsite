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
using TypicalSchoolWebsite_API.Models.PostLog;

namespace TypicalSchoolWebsite_API.Controllers
{
    [ApiController]
    [Route("postLog")]
    public class PostLogController : ControllerBase
    {
        private readonly IPostLogService _postLogService;


        public PostLogController(IPostLogService postLogService)
        {
            _postLogService = postLogService;   
        }


        [HttpGet("getAllPostLog")]
        public ActionResult<List<PostLogDTO>> GetAllPostLogDTO()
        {
            var operationResult = _postLogService.GetAllPostLog();

            return Ok(operationResult);
        }


        [HttpGet("getPostLogById/{id}")]
        public ActionResult<PostLogDTO> GetPostLogById([FromRoute] int id)
        {
            var postLogDTO = _postLogService.GetPostLogById(id);

            return Ok(postLogDTO);
        }
    }
}
