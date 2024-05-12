using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTOs;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;


namespace CodePulse.API.Controllers
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
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryRequestDTO createCategoryRequest)
        {
            var newCategory = new Category
            {
                Name = createCategoryRequest.Name,
                UrlHandle = createCategoryRequest.UrlHandle
            };

            await _categoryRepository.CreateAsync(newCategory);

            var response = new CategoryDTO
            {
                Id = newCategory.Id,
                Name = newCategory.Name,
                UrlHandle = newCategory.UrlHandle
            };

            return Ok(response);

        }

        //GET: https://localhost:7070/api/Categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            var response = new List<CategoryDTO>();
            //Map Domain model to DTO
            foreach (var category in categories)
            {
                response.Add(new CategoryDTO()
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                });
            }
            return Ok(response);
        }
    }
}
