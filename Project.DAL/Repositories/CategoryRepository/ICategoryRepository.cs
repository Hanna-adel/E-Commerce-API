using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllWithProductsAsync();
        Task<Category?> GetByIdWithProductsAsync(int id);
    }
}
