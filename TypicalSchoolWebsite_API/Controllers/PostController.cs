﻿using Microsoft.AspNetCore.Authorization;
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
        public ActionResult CreatePost([FromBody] CreatePostDTO dto)
        {
            var operationResult = _postService.CreatePost(dto, User);

            return Created(operationResult.ToString(), null);
        }


        [HttpGet("getAllPosts")]
        public ActionResult<List<PostDTO>> GetAllPosts()
        {
            var operationResult = _postService.GetAllPosts();

            return Ok(operationResult);
        }
    }
}