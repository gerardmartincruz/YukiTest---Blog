using Blog.Context;
using Blog.Entities;
using Blog.Services;
using Microsoft.EntityFrameworkCore;
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
            var post1 = new Post { Id = 1, Content = "Test Content1", Description = "asdasd1", Title = "my title", AuthorId = 1, Author = new Author() { Id = 1, Name = "name 1", Surname = "surname 1" } };
            var post2 = new Post { Id = 2, Content = "Test Content2", Description = "asdasd2", Title = "my title", AuthorId = 2, Author = new Author() { Id = 2, Name = "name 2", Surname = "surname 2" } };
            var post3 = new Post { Id = 3, Content = "Test Content3", Description = "asdasd3", Title = "my title", AuthorId = 3, Author = new Author() { Id = 3, Name = "name 3", Surname = "surname 3" } };
            var post4 = new Post { Id = 4, Content = "Test Content4", Description = "asdasd4", Title = "my title", AuthorId = 4, Author = new Author() { Id = 4, Name = "name 4", Surname = "surname 4" } };
            
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
        
        [Test]
        public async Task GetPostWithAuthor_WhenPostExists_ReturnsPost()
        {

            var options = new DbContextOptionsBuilder<BlogContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            // Arrange
            var post1 = new Post { Id = 5, Content = "Test Content5", Description = "asdasd5", Title = "my title", AuthorId = 5, Author = new Author() { Id = 5, Name = "name 5", Surname = "surname 5" } };
            var post2 = new Post { Id = 6, Content = "Test Content6", Description = "asdasd6", Title = "my title", AuthorId = 6, Author = new Author() { Id = 6, Name = "name 6", Surname = "surname 6" } };
            var post3 = new Post { Id = 7, Content = "Test Content7", Description = "asdasd7", Title = "my title", AuthorId = 7, Author = new Author() { Id = 7, Name = "name 7", Surname = "surname 7" } };
            var post4 = new Post { Id = 8, Content = "Test Content8", Description = "asdasd8", Title = "my title", AuthorId = 8, Author = new Author() { Id = 8, Name = "name 8", Surname = "surname 8" } };

            var postModel1 = new PostModel { Id = 5, Content = "Test Content5", Description = "asdasd5", Title = "my title", AuthorId = 5, Author = new AuthorModel() { Id = 5, Name = "name 5", Surname = "surname 5" } };
            var postModel2 = new PostModel { Id = 6, Content = "Test Content6", Description = "asdasd6", Title = "my title", AuthorId = 6, Author = new AuthorModel() { Id = 6, Name = "name 6", Surname = "surname 6" } };
            var postModel3 = new PostModel { Id = 7, Content = "Test Content7", Description = "asdasd7", Title = "my title", AuthorId = 7, Author = new AuthorModel() { Id = 7, Name = "name 7", Surname = "surname 7" } };
            var postModel4 = new PostModel { Id = 8, Content = "Test Content8", Description = "asdasd8", Title = "my title", AuthorId = 8, Author = new AuthorModel() { Id = 8, Name = "name 8", Surname = "surname 8" } };

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
            var result1 = await postReadService.GetPostWithAuthor(5);
            var result2 = await postReadService.GetPostWithAuthor(6);
            var result3 = await postReadService.GetPostWithAuthor(7);
            var result4 = await postReadService.GetPostWithAuthor(8);

            // Assert
            //Assert.Equals(post, result);
            Assert.That(postModel1, Is.EqualTo(result1));
            Assert.That(postModel2, Is.EqualTo(result2));
            Assert.That(postModel3, Is.EqualTo(result3));
            Assert.That(postModel4, Is.EqualTo(result4));
        }

        [Test]
        public async Task GetPostWithAuthor_WhenPostDontExists_ReturnsException()
        {

            var options = new DbContextOptionsBuilder<BlogContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            var mockContext = new BlogContext(options);

            var postReadService = new PostReadService(mockContext);

            // Assert
            Assert.ThrowsAsync<System.InvalidOperationException>(() => postReadService.GetPostWithAuthor(2));
        }

        [Test]
        public async Task GetPostWithoutAuthor_WhenPostDontExists_ReturnsException()
        {

            var options = new DbContextOptionsBuilder<BlogContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            var mockContext = new BlogContext(options);

            var postReadService = new PostReadService(mockContext);

            // Assert
            Assert.ThrowsAsync<System.InvalidOperationException>(() => postReadService.GetPostWithoutAuthor(2));
        }

    }
}
