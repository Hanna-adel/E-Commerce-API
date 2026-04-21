using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project.Common;

namespace Project.BLL
{
    public interface IOrderManager
    {
        Task<GeneralResult<IEnumerable<OrderReadDto>>> GetUserOrderAsync(string userId);
        Task<GeneralResult<OrderReadDto>> GetOrderByIdAsync(int id, string userId);
        Task<GeneralResult<OrderReadDto>> PlaceOrderAsync(string userId, OrderCreateDto orderCreateDto);
        Task<GeneralResult<OrderReadDto>> UpdateOrderStatusAsync(int orderId, OrderStatusUpdateDto dto);
        Task<GeneralResult<bool>> CancelOrderAsync(string userId, int orderId);
    }
}
