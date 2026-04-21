using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BLL;
using Project.Common;

namespace Project.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager _productManager;
        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }
        [HttpGet]
        public async Task<ActionResult<GeneralResult<PagedResult<ProductReadDto>>>> GetAllProducts(
            [FromQuery] int? categoryId,
            [FromQuery] string? name,
            [FromQuery] string? search,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var pagination = new PaginationParameters
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var filter = new ProductFilterParameters
            {
                CategoryId = categoryId,
                Name = name,
                Search = search
            };


            var result = await _productManager.GetFilteredProductsAsync(pagination, filter);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResult<ProductReadDto>>> GetProductById(int id)
        {
            var result = await _productManager.GetProductByIdAsync(id);
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
        public async Task<ActionResult<GeneralResult<ProductReadDto>>> CreateProduct(ProductCreateDto productCreateDto)
        {
            var result = await _productManager.CreateProductAsync(productCreateDto);
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
        public async Task<ActionResult<GeneralResult<ProductReadDto>>> UpdateProduct(int id, ProductUpdateDto productUpdateDto)
        {
            var result = await _productManager.UpdateProductAsync(id, productUpdateDto);
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
        public async Task<ActionResult<GeneralResult>> DeleteProduct(int id)
        {
            var result = await _productManager.DeleteProductAsync(id);
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
