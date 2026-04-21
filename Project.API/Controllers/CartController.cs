using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BLL;
using Project.Common;
using System.Security.Claims;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartManager _cartManager;
        public CartController(ICartManager cartManager)
        {
            _cartManager = cartManager;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResult<CartReadDto>>> GetCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _cartManager.GetCartReadAsync(userId);
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
        public async Task<ActionResult<GeneralResult>> RemoveFromCart(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _cartManager.RemoveFromCartAsync(userId, productId);
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
        public async Task<ActionResult<GeneralResult<CartReadDto>>> AddToCart(CartCreateDto cartCreateDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _cartManager.AddToCartAsync(userId, cartCreateDto);
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

        [HttpPut]
        public async Task<ActionResult<GeneralResult<CartReadDto>>> UpdateCart(CartCreateDto cartCreateDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _cartManager.UpdateCartAsync(userId, cartCreateDto);
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
