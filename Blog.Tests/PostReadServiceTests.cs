using Blog.Context;
using Blog.Entities;
using Blog.Services;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using System.Net.Sockets;
using System;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Blog.Models;

namespace Blog.Tests
{
    [TestFixture]
    public class PostReadServiceTests
    {
        [Test]
        public async Task GetPostWithoutAuthor_WhenPostExists_ReturnsPost()
        {

            var options = new DbContextOptionsBuilder<BlogContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            // Arrange
            var post1 = new Post { Id = 1, Content = "Test Content1", Description = "asdasd1", Title = "my title" };
            var post2 = new Post { Id = 2, Content = "Test Content2", Description = "asdasd2", Title = "my title" };
            var post3 = new Post { Id = 3, Content = "Test Content3", Description = "asdasd3", Title = "my title" };
            var post4 = new Post { Id = 4, Content = "Test Content4", Description = "asdasd4", Title = "my title" };
            
            var postModel1 = new PostModel { Id = 1, Content = "Test Content1", Description = "asdasd1", Title = "my title" };
            var postModel2 = new PostModel { Id = 2, Content = "Test Content2", Description = "asdasd2", Title = "my title" };
            var postModel3 = new PostModel { Id = 3, Content = "Test Content3", Description = "asdasd3", Title = "my title" };
            var postModel4 = new PostModel { Id = 4, Content = "Test Content4", Description = "asdasd4", Title = "my title" };

            var data = new List<Post>() 
            {
                post1,
                post2,
                post3,
                post4
            };
               
            var mockContext = new BlogContext(options);

            mockContext.Post.AddRange(data);
            mockContext.SaveChanges();

            var postReadService = new PostReadService(mockContext);


            // Act
            var result1 = await postReadService.GetPostWithoutAuthor(1);
            var result2 = await postReadService.GetPostWithoutAuthor(2);
            var result3 = await postReadService.GetPostWithoutAuthor(3);
            var result4 = await postReadService.GetPostWithoutAuthor(4);

            // Assert
            //Assert.Equals(post, result);
            Assert.That(postModel1, Is.EqualTo(result1));
            Assert.That(postModel2, Is.EqualTo(result2));
            Assert.That(postModel3, Is.EqualTo(result3));
            Assert.That(postModel4, Is.EqualTo(result4));
        }

    }
}
