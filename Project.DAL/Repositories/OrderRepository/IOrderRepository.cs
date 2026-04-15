
namespace Project.DAL
{
    public interface IOrderRepository:IGenericRepository<Order>
    {
        Task<IEnumerable<Order>>GetUserOrderAsync(string userId);
        Task<Order?> GetOrderWithItemAsync(int id);
    }
}
