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
using TypicalSchoolWebsite_API.Models.ImageFile;
using TypicalSchoolWebsite_API.Models.Post;
using TypicalSchoolWebsite_API.Models.PostLog;


namespace TypicalSchoolWebsite_API.Services
{
    public class PostService : IPostService
    {
        private readonly TSW_DbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IPostLogService _postLogService;
        private readonly IImageFileService _imageFileService;


        public PostService(
            TSW_DbContext dbContext,
            IMapper mapper,
            IAuthorizationService authorizationService,
            IPostLogService postLogService,
            IImageFileService imageFileService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _postLogService = postLogService;
            _imageFileService = imageFileService;
        }


        private Post GetPostEntityByAccessName(string accessName)
        {
            var post = _dbContext.Posts
                .Where(p => p.AccessName == accessName)
                    .Include(p => p.ImageFile)
                        .FirstOrDefault();

            if (post == null)
                throw new NotFoundException("No resource found");

            return post;
        }
        private Post GetPostEntityById(int id)
        {
            var post = _dbContext.Posts
                .Where(p => p.Id == id)
                    .Include(p => p.ImageFile)
                        .FirstOrDefault();

            if (post == null)
                throw new NotFoundException("No resource found");

            return post;
        }
        private int DeletePost(Post post, ClaimsPrincipal userClaims)
        {
            //PostLog
            var createPostLogDTO = new CreatePostLogDTO();
            createPostLogDTO.PreviousState = _mapper.Map<PostDTO>(post);
            createPostLogDTO.Operation = "DELETE";
            createPostLogDTO.UserId = Convert.ToInt32(userClaims.FindFirst(c => c.Type == "UserId").Value);


            if (post == null)
                throw new NotFoundException("No resource found");


            //Authorization
            var authorizationResult = _authorizationService.AuthorizeAsync(userClaims, post,
                new ResourceOperationRequirement(ResourceOperation.Delete));
            if (!authorizationResult.IsCompletedSuccessfully)
                throw new BadAuthorizationExeption();


            post.IsActive = false;
            _dbContext.SaveChanges();


            //CurrentState
            createPostLogDTO.CurrentState = _mapper.Map<PostDTO>(post);
            _postLogService.CreatePostLog(createPostLogDTO);


            return 0;
        }
        private PostDTO EditPost(Post post, EditPostDTO dto, ClaimsPrincipal userClaims)
        {
            //PostLog
            var createPostLogDTO = new CreatePostLogDTO();
            createPostLogDTO.PreviousState = _mapper.Map<PostDTO>(post);
            createPostLogDTO.Operation = "EDIT";
            createPostLogDTO.UserId = Convert.ToInt32(userClaims.FindFirst(c => c.Type == "UserId").Value);


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


            //CurrentState
            createPostLogDTO.CurrentState = _mapper.Map<PostDTO>(post);
            _postLogService.CreatePostLog(createPostLogDTO);


            var postDTO = _mapper.Map<PostDTO>(post);
            return postDTO;
        }
        public async Task<int> CreatePost(CreatePostDTO dto, ClaimsPrincipal userClaims)
        {
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

                AccessName = dto.Title.Replace("", "-"),

                CreationDate = timestamp,
                LastEditDate = timestamp,

                IsActive = true,

                UserId = userId,
            };


            //FileService
            var fileServiceDTO = new CreateImageFileDTO();
            fileServiceDTO.File = dto.PostImage;
            fileServiceDTO.FileName = dto.PostImage.FileName;
            fileServiceDTO.FileExtenstion = dto.PostImage.FileName.Split('.')[1];


            //fileService
            var fileServiceResult = await _imageFileService.CreateImage(fileServiceDTO);
            ImageFile imageFile = new ImageFile()
            {
                UserGivenName = dto.PostImage.FileName,
                StorageName = fileServiceResult,
                FileSize = dto.PostImage.Length,
                WhenCreated = DateTime.Now,

                UserId = userId
            };
            _dbContext.ImageFiles.Add(imageFile);
            _dbContext.SaveChanges();


            //Save Post
            newPost.ImageFileId = imageFile.Id;
            _dbContext.Posts.Add(newPost);
            _dbContext.SaveChanges();

            return newPost.Id;
        }


        public List<PostDTO> GetAllPosts()
        {
            var posts = _dbContext.Posts
                .Include(p => p.User)
                    .Include(p => p.User.Role)
                    .Include(p => p.ImageFile)
                        .ToList();

            if (posts.Count == 0)
                throw new NotFoundException("No resources found");


            var postsDTO = _mapper.Map<List<PostDTO>>(posts);

            return postsDTO;
        }


        public PostDTO GetPostById(int id)
        {
            var post = GetPostEntityById(id);

            var postDTO = _mapper.Map<PostDTO>(post);
            return postDTO;
        }


        public PostDTO GetPostByAccessName(string accessName)
        {
            var post = GetPostEntityByAccessName(accessName);

            var postDTO = _mapper.Map<PostDTO>(post);
            return postDTO;
        }


        public PostQueryResultDTO GetPostsUsingQuery(PostQueryDTO dto)
        {
            var posts = _dbContext.Posts
                .Include(p => p.User)
                    .Include(p => p.User.Role)
                    .Include(p => p.ImageFile)
                        .ToList();


            //Sort
            posts.OrderBy(p => p.CreationDate);


            var retPosts = posts
                .Skip(dto.DesiredNumberOfItems * (dto.DesiredPage))
                    .Take(dto.DesiredNumberOfItems)
                        .ToList();

            if (retPosts.Count == 0)
                throw new NotFoundException();

            var postsDTO = _mapper.Map<List<PostDTO>>(retPosts);



            var maxPages = Convert.ToDouble(posts.Count() / dto.DesiredNumberOfItems);
            var returnDTO = new PostQueryResultDTO()
            {
                Posts = postsDTO,
                DesiredPage = dto.DesiredPage,
                DesiredNumberOfItems = dto.DesiredNumberOfItems,
                MaxPages = Convert.ToInt32(Math.Ceiling(maxPages))
            };


            return returnDTO;
        }


        public int DeletePostById(int id, ClaimsPrincipal userClaims)
        {
            var post = GetPostEntityById(id);

            return DeletePost(post, userClaims);
        }


        public int DeletePostByAccessName(string accessName, ClaimsPrincipal userClaims)
        {
            var post = GetPostEntityByAccessName(accessName);

            return DeletePost(post, userClaims);
        }


        public PostDTO EditPostById(EditPostDTO dto, ClaimsPrincipal userClaims)
        {
            var post = GetPostEntityById(dto.Id);

            return EditPost(post, dto, userClaims);
        }


        public PostDTO EditPostByAccessName(EditPostDTO dto, ClaimsPrincipal userClaims)
        {
            var post = GetPostEntityByAccessName(dto.AccessName);

            return EditPost(post, dto, userClaims);
        }
    }
}
