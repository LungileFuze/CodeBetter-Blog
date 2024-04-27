using CodeBetter.API.Data;
using CodeBetter.API.Models.Domain;
using CodeBetter.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodeBetter.API.Repositories.Implementation
{
    public class CategoryReposity : ICategoryRepository
    {
        private readonly CodeBetterDBContext _dbContext;
        public CategoryReposity(CodeBetterDBContext codeBetterDBContext)
        {
            _dbContext = codeBetterDBContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }
    }
}
