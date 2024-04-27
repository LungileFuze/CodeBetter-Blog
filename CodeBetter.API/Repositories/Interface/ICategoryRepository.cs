using CodeBetter.API.Models.Domain;

namespace CodeBetter.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
    }
}
