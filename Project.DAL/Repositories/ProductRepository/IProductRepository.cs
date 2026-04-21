using Project.Common;
using System;

namespace Project.DAL
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllWithCategoryAsync();
        Task<Product?> GetByIdWithCategoryAsync(int id);
        Task<PagedResult<Product>> GetFilteredProductsAsync(PaginationParameters pagination, ProductFilterParameters filter);
    }
}
