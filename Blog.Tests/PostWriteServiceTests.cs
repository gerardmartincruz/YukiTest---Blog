using Blog.Context;
using Blog.Entities;
using Blog.Services;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Blog.Tests
{
    [TestFixture]
    public class PostWriteServiceTests
    {
        [Test]
        public async Task CreatePost_WhenCalled_SavesPostToDatabase()
        {
            // Arrange
            var context = Substitute.For<BlogContext>();
            var postWriteService = new PostWriteService(context);

            var post = new Post { Content = "Test Content" };

            // Act
            await postWriteService.CreatePostWithAuthor(post);

            // Assert
            await context.Post.Received(1).AddAsync(post);
            await context.Received(1).SaveChangesAsync();
        }

        // Add similar tests for error scenarios and other behaviors
    }
}
