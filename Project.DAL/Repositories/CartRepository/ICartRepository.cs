using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL
{
    public interface ICartRepository:IGenericRepository<Cart>
    {
        Task<Cart?> GetCartByUserIdAsync(string userId);
        void RemoveCartItem(CartItem cartItem);
    }
}
