using CodeBetter.API.Data;
using CodeBetter.API.Models.Domain;
using CodeBetter.API.Models.DTO;
using CodeBetter.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Writer")]
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

        //GET: https://localhost:7159/api/Categories?query=html&sortBy=name&sortDirection=desc
        [HttpGet]
       
        public async Task<IActionResult> GetAllCategories(
            [FromQuery] string? query,
            [FromQuery] string? sortBy, 
            [FromQuery] string? sortDirection,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            var categories = await _categoryRepository.GetAllAsync(query, sortBy,
                sortDirection, pageNumber, pageSize);

            // Map Domain model to DTO
            var categoryDtos = new List<CategoryDto>();
            foreach (var category in categories) 
            {
                categoryDtos.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle,
                });
            }

            return Ok(categoryDtos);
        }

        //GET: https://localhost:7159/api/categories/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id);

            if(existingCategory is null)
            {
                return NotFound();
            }

            var categoryDto = new CategoryDto
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle
            };
            return Ok(categoryDto);
        }

        //GET: https://localhost:7159/api/categories/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> EditCategory([FromRoute] Guid id, UpdateCategoryRequestDTO updateCategoryRequestDTO)
        {
            //Convert DTO to Domain Model
            var category = new Category
            {
                Id = id,
                Name = updateCategoryRequestDTO.Name,
                UrlHandle = updateCategoryRequestDTO.UrlHandle,
            };

            category = await _categoryRepository.UpdateAsync(category);

            if(category is null)
            {
                return NotFound();
            }

            //Convert Domain model to DTO
            var categoryDTO = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };

            return Ok(categoryDTO);
        }

        //GET: https://localhost:7159/api/categories/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var category = await _categoryRepository.DeleteAsync(id);

            if(category is null)
            {
                return NotFound();
            }

            //Convert Domain model to DTO
            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };
            return Ok(categoryDto);
        }

        //GET: https://localhost:7226/api/categories/count
        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetCategoriesTotal()
        {
            int count = await _categoryRepository.GetCount();

            return Ok(count);
        }
    }
}
