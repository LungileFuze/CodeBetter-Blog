using CodeBetter.API.Data;
using CodeBetter.API.Models.Domain;
using CodeBetter.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodeBetter.API.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {

        private readonly CodeBetterDBContext _dbContext;
        public BlogPostRepository(CodeBetterDBContext codeBetterDBContext)
        {
            _dbContext = codeBetterDBContext;
        }
        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await _dbContext.BlogPosts.AddAsync(blogPost);
            await _dbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlogPost = await _dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBlogPost != null)
            {
                _dbContext.BlogPosts.Remove(existingBlogPost);
                await _dbContext.SaveChangesAsync();
                return existingBlogPost;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _dbContext.BlogPosts.Include(x => x.Categories).ToListAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return await _dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await _dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);    
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await _dbContext.BlogPosts.Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlogPost != null)
            {
                //Update BlogPost
                _dbContext.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);

                //update Categories
                existingBlogPost.Categories = blogPost.Categories;
                await _dbContext.SaveChangesAsync();
                return blogPost;
            }
            return null;


        }
    }
}
