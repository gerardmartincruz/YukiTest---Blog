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
            try
            {
                // _context.Author.Add(post.Author);
                //await _context.SaveChangesAsync();

                _context.Post.Add(post);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        
        public async Task CreatePostWithoutAuthor(Post post)
        {
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
