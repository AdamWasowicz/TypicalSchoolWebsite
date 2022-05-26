using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Entities;
using TypicalSchoolWebsite_API.Exceptions;
using TypicalSchoolWebsite_API.Interfaces;
using TypicalSchoolWebsite_API.Models.Post;

namespace TypicalSchoolWebsite_API.Services
{
    public class PostService : IPostService
    {
        private readonly TSW_DbContext _dbContext;
        private readonly IMapper _mapper;


        public PostService(
            TSW_DbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public int CreatePost(CreatePostDTO dto, ClaimsPrincipal userClaims)
        {
            if (userClaims.Claims.Count() == 0)
                throw new BadAuthorizationExeption("No calims found");


            int userId = Convert.ToInt32(userClaims.FindFirst(c => c.Type == "UserId").Value);
            var timestamp = DateTime.Now;


            var newPost = new Post()
            {
                Title = dto.Title,
                TextContent = dto.TextContent,

                CreationDate = timestamp,
                LastEditDate = timestamp,

                IsActive = false,

                UserId = userId,
            };


            _dbContext.Posts.Add(newPost);
            _dbContext.SaveChanges();

            return newPost.Id;
        }
    

        public List<PostDTO> GetAllPosts()
        {
            var posts = _dbContext.Posts
                .Include(p => p.User)
                    .Include(p => p.User.Role)
                    .ToList();

            if (posts.Count == 0)
                throw new NotFoundException("No resources found");


            var postsDTO = _mapper.Map<List<PostDTO>>(posts);

            return postsDTO;
        }
    }
}
