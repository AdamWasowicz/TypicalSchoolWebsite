using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Authorization.Requirements;
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
        private readonly IAuthorizationService _authorizationService;

        public PostService(
            TSW_DbContext dbContext,
            IMapper mapper,
            IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }


        public int CreatePost(CreatePostDTO dto, ClaimsPrincipal userClaims)
        {
            //if (!userClaims.Claims.Any())
            //    throw new BadAuthorizationExeption("No calims found");


            //Authorization
            var authorizationResult = _authorizationService.AuthorizeAsync(userClaims, null,
                new ResourceOperationRequirement(ResourceOperation.Create));
            if (!authorizationResult.IsCompletedSuccessfully)
                throw new BadAuthorizationExeption();


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


        public PostDTO GetPostById(int id)
        {
            var post = _dbContext.Posts
                .Where(p => p.Id == id)
                    .FirstOrDefault();


            if (post == null)
                throw new NotFoundException("No resource found");


            var postDTO = _mapper.Map<PostDTO>(post);
            return postDTO;
        }


        public int DeletePostById(int id, ClaimsPrincipal userClaims)
        {
            var post = _dbContext.Posts
                .Where(p => p.Id == id)
                    .FirstOrDefault();


            if (post == null)
                throw new NotFoundException("No resource found");


            //Authorization
            var authorizationResult = _authorizationService.AuthorizeAsync(userClaims, post,
                new ResourceOperationRequirement(ResourceOperation.Delete));
            if (!authorizationResult.IsCompletedSuccessfully)
                throw new BadAuthorizationExeption();


            _dbContext.Posts.Remove(post);
            _dbContext.SaveChanges();
            return 0;
        }


        public PostDTO EditPostById(EditPostDTO dto, ClaimsPrincipal userClaims)
        {
            var post = _dbContext.Posts
                .Where(p => p.Id == dto.Id)
                    .FirstOrDefault();


            //Authorization
            var authorizationResult = _authorizationService.AuthorizeAsync(userClaims, post,
                new ResourceOperationRequirement(ResourceOperation.Update));
            if (!authorizationResult.IsCompletedSuccessfully)
                throw new BadAuthorizationExeption();


            //Changes
            post.IsActive = false;
            post.Title = dto.Title;
            post.TextContent = dto.TextContent;

            //PostCategory
            var pc = _dbContext.PostCategories
                .Where(x => x.Id == dto.Id)
                    .FirstOrDefault();

            if (pc == null)
                throw new NotFoundException();

            post.PostCategoryId = dto.PostCategoryId;

            //Other
            post.LastEditDate = DateTime.Now;


            _dbContext.SaveChanges();


            var postDTO = _mapper.Map<PostDTO>(post);
            return postDTO;
        }
    }
}
