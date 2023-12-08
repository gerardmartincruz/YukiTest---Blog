using Blog.Entities;

namespace Blog.Services
{
    public interface IPostWriteService
    {
        Task CreatePostWithAuthor(Post post);
        Task CreatePostWithoutAuthor(Post post);
    }
}