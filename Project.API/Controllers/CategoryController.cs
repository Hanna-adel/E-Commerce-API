using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BLL;
using Project.Common;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;
        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResult<IEnumerable<CategoryReadDto>>>> GetAllCategories()
        {
            var result = await _categoryManager.GetAllCategoriesAsync();
            if (!result.Success)
            {
                if (result.Errors != null)
                {
                    return BadRequest(result);
                }
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResult<CategoryReadDto>>> GetCategoryById(int id)
        {
            var result = await _categoryManager.GetCategoryByIdAsync(id);
            if (!result.Success)
            {
                if (result.Errors != null)
                {
                    return BadRequest(result);
                }
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<GeneralResult<CategoryReadDto>>> CreateCategory(CategoryCreateDto categoryCreateDto)
        {
            var result = await _categoryManager.CreateCategoryAsync(categoryCreateDto);
            if (!result.Success)
            {
                if (result.Errors != null)
                {
                    return BadRequest(result);
                }
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<GeneralResult<CategoryReadDto>>> UpdateCategory(int id, CategoryUpdateDto categoryUpdateDto)
        {
            var result = await _categoryManager.UpdateCategoryAsync(id, categoryUpdateDto);
            if (!result.Success)
            {
                if (result.Errors != null)
                {
                    return BadRequest(result);
                }
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<GeneralResult>> DeleteCategory(int id)
        {
            var result = await _categoryManager.DeleteCategoryAsync(id);
            if (!result.Success)
            {
                if (result.Errors != null)
                {
                    return BadRequest(result);
                }
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
