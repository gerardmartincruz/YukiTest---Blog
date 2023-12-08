using Blog.Context;
using Blog.Entities;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services
{
    public class PostReadService : IPostReadService
    {
        private readonly BlogContext _context;

        public PostReadService(BlogContext context)
        {
            _context = context;

            //_context.
        }

        public async Task<PostModel> GetPostWithoutAuthor(int postId)
        {
            var result = await (from post in _context.Post
                                where post.Id == postId
                                select new PostModel()
                                {
                                    Id = post.Id,
                                    Title = post.Title,
                                    Description = post.Description,
                                    Content = post.Content
                                }).SingleAsync();

            if (result is not null)
            {
                return result;
            }

            throw new Exception("Post Not Found");
        }

        public async Task<PostModel> GetPostWithAuthor(int postId)
        {
            try
            {
               var result = await (from post in _context.Post
                       where post.Id == postId
                       select new PostModel()
                       {
                           Id = post.Id,
                           Title = post.Title,
                           Description = post.Description,
                           Content = post.Content,
                           AuthorId = post.AuthorId,
                           Author = (from author in _context.Author
                                     where author.Id == post.AuthorId
                                     select new AuthorModel()
                                     {
                                         Id = author.Id,
                                         Name = author.Name,
                                         Surname = author.Surname
                                     }).Single()

                       }).SingleAsync();

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

    }
}
