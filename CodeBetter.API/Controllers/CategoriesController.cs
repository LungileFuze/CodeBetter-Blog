using CodeBetter.API.Data;
using CodeBetter.API.Models.Domain;
using CodeBetter.API.Models.DTO;
using CodeBetter.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeBetter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto _createRequest)
        {
            //Map DTO to Domain Model
            var category = new Category
            {
                Name = _createRequest.Name,
                UrlHandle = _createRequest.UrlHandle,
            };

            await _categoryRepository.CreateAsync(category);

            //Map Domain Model back to DTO
            var categoryResponse = new CategoryDto
            {
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };

            return Ok(categoryResponse);
        }
    }
}
