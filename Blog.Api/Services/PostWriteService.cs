using Blog.Context;
using Blog.Entities;

namespace Blog.Services
{
    public class PostWriteService : IPostWriteService
    {
        private readonly BlogContext _context;

        public PostWriteService(BlogContext context)
        {
            _context = context;
        }

        public async Task CreatePostWithAuthor(Post post)
        {
            if (post is null)
            {
                throw new ArgumentNullException("Post can't be null");
            }

            try
            {
                await _context.Post.AddAsync(post);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        
        public async Task CreatePostWithoutAuthor(Post post)
        {
            if (post is null)
            {
                throw new ArgumentNullException("Post can't be null");
            }

            try
            {
                await _context.Post.AddAsync(post);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
