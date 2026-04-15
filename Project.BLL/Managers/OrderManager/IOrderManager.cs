using Project.Common;

namespace Project.BLL
{
    public interface IOrderManager
    {
        Task<GeneralResult<IEnumerable<OrderReadDto>>> GetUserOrderAsync(string userId);
        Task<GeneralResult<OrderReadDto>> GetOrderByIdAsync(int id, string userId);
        Task<GeneralResult<OrderReadDto>> PlaceOrderAsync(OrderCreateDto orderCreateDto, string userId);
    }
}
