using Blog.Context;
using Blog.Entities;
using Blog.Services;
using NSubstitute;

namespace Blog.Tests
{
    [TestFixture]
    public class PostWriteServiceTests
    {
        [Test]
        public async Task CreatePostWithAuthor_WhenCalled_SavesPostToDatabase()
        {
            // Arrange
            var context = Substitute.For<BlogContext>();
            var postWriteService = new PostWriteService(context);

            var post = new Post { Id = 1234, Description="descrition", Title ="title", Content = "Test Content", AuthorId = 123, Author = new Author() { Id = 123, Name="author name", Surname = "Author surname" } };

            // Act
            await postWriteService.CreatePostWithAuthor(post);

            // Assert
            await context.Post.Received(1).AddAsync(post);
            await context.Received(1).SaveChangesAsync();
        }

        [Test]
        public async Task CreatePostWithoutAuthor_WhenCalled_SavesPostToDatabase()
        {
            // Arrange
            var context = Substitute.For<BlogContext>();
            var postWriteService = new PostWriteService(context);

            var post = new Post { Id = 1234, Description = "descrition", Title = "title", Content = "Test Content" };

            // Act
            await postWriteService.CreatePostWithoutAuthor(post);

            // Assert
            await context.Post.Received(1).AddAsync(post);
            await context.Received(1).SaveChangesAsync();
        }

        [Test]
        public async Task CreatePostWithAuthor_WhenCalledWithNullPost_ThrowsException()
        {
            // Arrange
            var context = Substitute.For<BlogContext>();
            var postWriteService = new PostWriteService(context);

            Post post = null;

            // Assert
            Assert.ThrowsAsync<System.ArgumentNullException>(() => postWriteService.CreatePostWithAuthor(post));
        }

        [Test]
        public async Task CreatePostWithoutAuthor_WhenCalledWithNullPost_ThrowsException()
        {
            // Arrange
            var context = Substitute.For<BlogContext>();
            var postWriteService = new PostWriteService(context);

            Post post = null;

            // Assert
            Assert.ThrowsAsync<System.ArgumentNullException>(() => postWriteService.CreatePostWithoutAuthor(post));
        }
    }
}
