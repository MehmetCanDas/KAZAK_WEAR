using Catalog.Dtos.CateoryDtos;
using Microsoft.AspNetCore.Mvc;
namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly Services.CategoryServices.ICategoryService _categoryService;

        public CategoriesController(Services.CategoryServices.ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            try
            {
                var values = await _categoryService.GetAllCategoryAsync();
                return Ok(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("Category ID cannot be null or empty.");
                }

                var result = await _categoryService.GetByIdCategoryAsync(id);
                if (result == null)
                {
                    return NotFound($"Category with ID {id} not found.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            try
            {
                if (createCategoryDto == null)
                {
                    return BadRequest("Category data is null.");
                }

                await _categoryService.CreateCategoryAsync(createCategoryDto);
                return Ok("Category successfully created.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("Category ID cannot be null or empty.");
                }

                await _categoryService.DeleteCategoryAsync(id);
                return Ok("Category successfully deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto updateCategoryDto)
        {
            try
            {
                if (updateCategoryDto == null)
                {
                    return BadRequest("Category data is null.");
                }

                await _categoryService.UpdateCategoryAsync(updateCategoryDto);
                return Ok("Category successfully updated.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
