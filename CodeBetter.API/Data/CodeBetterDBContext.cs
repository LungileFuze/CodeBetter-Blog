using CodeBetter.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CodeBetter.API.Data
{
    public class CodeBetterDBContext : DbContext
    {
        public CodeBetterDBContext(DbContextOptions<CodeBetterDBContext> options) : base(options)
        {
        }
        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<BlogImage> BlogImages { get; set; }
    }
}
