using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context):base(context) { }
        public async Task<Order?> GetOrderWithItemAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(p => p.Products)
                .FirstOrDefaultAsync(o =>o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetUserOrderAsync(string userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(p => p.Products)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }
    }
}
