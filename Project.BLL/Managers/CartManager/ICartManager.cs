using Project.Common;

namespace Project.BLL
{
    public interface ICartManager
    {
        Task<GeneralResult<CartReadDto>> GetCartReadAsync(string userId);
        Task<GeneralResult<CartReadDto>> AddToCartAsync(string userId, CartCreateDto cartCreateDto);
        Task<GeneralResult<CartCreateDto>> UpdateCartAsync(string userId, CartCreateDto cartCreateDto);
        Task<GeneralResult> RemoveFromCartAsync(string userId, int productId);
    }
}
