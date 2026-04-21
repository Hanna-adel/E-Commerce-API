

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BLL;
using Project.Common;
using System.Security.Claims;

namespace Project.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager _orderManager;
        public OrderController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResult<IEnumerable<OrderReadDto>>>> GetAllOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _orderManager.GetUserOrderAsync(userId);
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
        public async Task<ActionResult<GeneralResult<OrderReadDto>>> GetOrderById(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _orderManager.GetOrderByIdAsync(id, userId);
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
        public async Task<ActionResult<GeneralResult<OrderReadDto>>> CreateOrder(OrderCreateDto orderCreateDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _orderManager.PlaceOrderAsync(userId, orderCreateDto);
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
        public async Task<ActionResult<GeneralResult<string>>> CancelOrder(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _orderManager.CancelOrderAsync(userId, id);
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
        public async Task<ActionResult<GeneralResult<OrderReadDto>>> UpdateOrderStatus(int orderId, OrderStatusUpdateDto dto)
        {
            var result = await _orderManager.UpdateOrderStatusAsync(orderId, dto);
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
