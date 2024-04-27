using CodeBetter.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CodeBetter.API.Data
{
    public class CodeBetterDBContext : DbContext
    {
        public CodeBetterDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
