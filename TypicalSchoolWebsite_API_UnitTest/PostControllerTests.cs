using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using TypicalSchoolWebsite_API.Controllers;
using TypicalSchoolWebsite_API.Entities;
using TypicalSchoolWebsite_API.Exceptions;
using TypicalSchoolWebsite_API.Interfaces;
using Xunit;


namespace TypicalSchoolWebsite_API_UnitTest
{
    public class PostControllerTests
    {
        private TSW_DbContext _dbContext;


        //GetAllPosts
        [Fact]
        public void GetAllPosts_Throws_Exception_When_Service_Throws_Exception()
        {
            //Arrange
            var serviceStub = new Mock<IPostService>();
            serviceStub.Setup(repo => repo.GetAllPosts()).Throws(new NotFoundException());

            var controller = new PostController(serviceStub.Object);


            //Act & Assert
            Assert.Throws<NotFoundException>(() => controller.GetAllPosts());
        }

        [Fact]
        public void GetAllPosts_Returns_Posts_After_Inserting_Them()
        {
            //Arrange
            //DataBase Setup
            var builder = new DbContextOptionsBuilder<TSW_DbContext>();
            builder.UseInMemoryDatabase(databaseName: "UnitTestDatabase");
            var dbContextOptions = builder.Options;
            _dbContext = new TSW_DbContext(dbContextOptions);

            //Create records
            var timestamp = DateTime.Now;
            var postList = new List<Post>
            {
                new Post()
                {
                    Title = "TestTitle1",
                    TextContent = "TestTextContent1",

                    CreationDate = timestamp,
                    LastEditDate = timestamp,

                    IsActive = false,

                    UserId = 1,
                },

                new Post()
                {
                    Title = "TestTitle2",
                    TextContent = "TestTextContent2",

                    CreationDate = timestamp,
                    LastEditDate = timestamp,

                    IsActive = false,

                    UserId = 2,
                },
            };

            //Add records
            _dbContext.Posts.AddRange(postList);
            _dbContext.SaveChanges();


            //Stub
            var serviceStub = new Mock<IPostService>();
            serviceStub.Setup(repo => repo.GetAllPosts()).Returns(async () => 
            {
                var posts = await _dbContext.Posts.ToListAsync();
                return posts;
            });

            var controller = new PostController(serviceStub.Object);



            //Act & Assert
            Assert.True(controller.GetAllPosts().Value.Count == 2);


            //Cleanup
            _dbContext.Database.EnsureDeleted();
        }



        //GetPostById
        //[Fact]
        //public async void GetPostById_


        //Other
        [Fact]
        public async void When_There_Is_Post_In_Db_Then_Count_Should_Be_Higher_Than_0()
        {
            //Arrange
            //DataBase Setup
            var builder = new DbContextOptionsBuilder<TSW_DbContext>();
            builder.UseInMemoryDatabase(databaseName: "UnitTestDatabase");
            var dbContextOptions = builder.Options;
            _dbContext = new TSW_DbContext(dbContextOptions);


            //Add one record
            var timestamp = DateTime.Now;
            var newPost = new Post()
            {
                Title = "TestTitle",
                TextContent = "TestTextContent",

                CreationDate = timestamp,
                LastEditDate = timestamp,

                IsActive = false,

                UserId = 1,
            };

            _dbContext.Posts.Add(newPost);
            _dbContext.SaveChanges();


            //Act
            var result = await _dbContext.Posts.ToListAsync();


            //Assert
            Assert.True(result.Count > 0);


            //Cleanup
            _dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async void When_There_Is_No_Posts_In_Db_Then_Count_Should_Be_0()
        {
            //Arrange
            //DataBase Setup
            var builder = new DbContextOptionsBuilder<TSW_DbContext>();
            builder.UseInMemoryDatabase(databaseName: "UnitTestDatabase");
            var dbContextOptions = builder.Options;
            _dbContext = new TSW_DbContext(dbContextOptions);


            //Act
            var result = await _dbContext.Posts.ToListAsync();


            //Assert
            Assert.True(result.Count == 0);


            //Cleanup
            _dbContext.Database.EnsureDeleted();
        }


    }
}
