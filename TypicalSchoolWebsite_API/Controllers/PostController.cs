using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Interfaces;
using TypicalSchoolWebsite_API.Models.Post;


namespace TypicalSchoolWebsite_API.Controllers
{
    [ApiController]
    [Route("api/post")]
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


        [HttpGet("getPostByAccessName/{accessName}")]
        public ActionResult<PostDTO> GetPostById([FromRoute] string accessName)
        {
            var postDTO = _postService.GetPostByAccessName(accessName);

            return Ok(postDTO);
        }


        [HttpPost("getPosts/query")]
        public ActionResult<PostQueryResultDTO> GetPostsUsingQuery([FromBody] PostQueryDTO dto)
        {
            var result = _postService.GetPostsUsingQuery(dto);

            return Ok(result);
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


        [HttpDelete("deletePostByAccessName/{accessName}")]
        [Authorize(Policy = "IsWriter")]
        public ActionResult DeletePostById([FromRoute] string accessName)
        {
            var result = _postService.DeletePostByAccessName(accessName, User);

            if (result != 0)
                return StatusCode(500);

            return NoContent();
        }


        [HttpPut("editPostById")]
        [Authorize(Policy = "IsWriter")]
        public ActionResult<PostDTO> EditPostById([FromBody] EditPostDTO dto)
        {
            var postDTO = _postService.EditPostById(dto, User);

            return Ok(postDTO);
        }


        [HttpPut("editPostByAccessName")]
        [Authorize(Policy = "IsWriter")]
        public ActionResult<PostDTO> EditPostByAccessName([FromBody] EditPostDTO dto)
        {
            var postDTO = _postService.EditPostById(dto, User);

            return Ok(postDTO);
        }
    }
}
