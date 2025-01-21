using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            this._context = dbContext;
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await _context.BlogPosts.AddAsync(blogPost);
            await _context.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid postId)
        {
           var existingPost = await _context.BlogPosts.FirstOrDefaultAsync(x => x.Id.Equals(postId));
            if (existingPost != null) { 
                _context.BlogPosts.Remove(existingPost);
                await _context.SaveChangesAsync();
            }
            return existingPost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _context.BlogPosts.Include(x => x.Categories).ToListAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(Guid postId)
        {
            return await _context.BlogPosts.Include(bp => bp.Categories).FirstOrDefaultAsync(bp => bp.Id == postId);

        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            _context.Entry(blogPost).CurrentValues.SetValues(blogPost);
            await _context.SaveChangesAsync();
            return blogPost;
        }
    }
}
