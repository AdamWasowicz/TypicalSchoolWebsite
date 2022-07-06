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
    [Route("post")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;


        public PostController(IPostService postService)
        {
            _postService = postService;
        }


        [HttpPost("createPost")]
        [Authorize(Policy = "IsWriter")]
        public async Task<ActionResult> CreatePost([FromForm] CreatePostDTO dto)
        {
            var operationResult =  await _postService.CreatePost(dto, User);

            return Created(operationResult.ToString(), null);
        }


        [HttpGet("getAllPosts")]
        public ActionResult<List<PostDTO>> GetAllPosts()
        {
            var operationResult = _postService.GetAllPosts();

            return Ok(operationResult);
        }


        [HttpGet("getPostById/{id}")]
        public ActionResult<PostDTO> GetPostById([FromRoute] int id)
        {
            var postDTO = _postService.GetPostById(id);

            return Ok(postDTO);
        }


        [HttpDelete("deletePostById/{id}")]
        [Authorize(Policy = "IsWriter")]
        public ActionResult DeletePostById([FromRoute] int id)
        {
            var result = _postService.DeletePostById(id, User);

            if (result != 0)
                return StatusCode(500);

            return NoContent();
        }


        [HttpPut("editPost")]
        [Authorize(Policy = "IsWriter")]
        public ActionResult<PostDTO> EditPost([FromBody] EditPostDTO dto)
        {
            var postDTO = _postService.EditPostById(dto, User);

            return Ok(postDTO);
        }
    }
}
