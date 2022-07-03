using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Authorization.Requirements;
using TypicalSchoolWebsite_API.Controllers;
using TypicalSchoolWebsite_API.Entities;
using TypicalSchoolWebsite_API.Exceptions;
using TypicalSchoolWebsite_API.Interfaces;
using TypicalSchoolWebsite_API.Models.Post;
using TypicalSchoolWebsite_API.Services;
using Xunit;


namespace TypicalSchoolWebsite_API_UnitTest
{
    public class PostServiceTests
    {
        private TSW_DbContext _dbContext;


        public void SetupDatabase()
        {
            var builder = new DbContextOptionsBuilder<TSW_DbContext>();
                builder.UseInMemoryDatabase(databaseName: "UnitTestDatabase");
            var dbContextOptions = builder.Options;
            _dbContext = new TSW_DbContext(dbContextOptions);
        }

        public void DeleteDatabase()
        {
            _dbContext.Database.EnsureDeleted();
        }

        


        //CreatePost
        [Fact]
        public async void CreatePost_Creates_Post_In_Db()
        {
            //Arrange
            //Database
            SetupDatabase();

            //CreatePostDTO
            CreatePostDTO dto = new CreatePostDTO()
            {
                Title = "TestTitle",
                TextContent = "TestTextContent"
            };

            //IMapper
            var mapperService = new Mock<IMapper>();
                 mapperService.Setup(repo => repo.Map<Post>(dto)).Returns(new Post()
                 {
                     Title = dto.Title,
                     TextContent = dto.TextContent
                 });

            //IAuthorizationService
            //args
            ClaimsPrincipal userClaims = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim("UserName", "UserName"),
                new Claim("FirstName", "FurstName"),
                new Claim("Surname", "Surname"),
                new Claim("UserId", 1.ToString()),
                new Claim("RoleId", 1.ToString()),
            }));

            var resReq = new ResourceOperationRequirement(ResourceOperation.Create);
            var func = ((Func<Func<AuthorizationResult>>)(() => { return AuthorizationResult.Success; }))();
            var task = new Task<AuthorizationResult>(func);
            var authService = new Mock<IAuthorizationService>();
            //authService.Setup(repo => repo.AuthorizeAsync(userClaims, null, resReq)).Returns(task);

            //PostService
            var postService = new PostService(_dbContext, mapperService.Object, authService.Object, null);



            //Act
            var posts = await _dbContext.Posts.ToListAsync();
            var createResult = postService.CreatePost(dto, userClaims);



            //Assert
            Assert.True(createResult != -1);
            Assert.True(posts != null);
            Assert.StrictEqual(1, createResult);



            //Cleanup
            DeleteDatabase();
        }
    }
}
