using Blog.Entities;
using Blog.Models;

namespace Blog.Services
{
    public interface IPostReadService
    {
        Task<PostModel> GetPostWithAuthor(int postId);
        Task<PostModel> GetPostWithoutAuthor(int postId);
    }
}