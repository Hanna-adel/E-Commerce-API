using Microsoft.EntityFrameworkCore;
using Project.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL
{
    public class ProductRepository:GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }
        public async Task<IEnumerable<Product>> GetAllWithCategoryAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdWithCategoryAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<PagedResult<Product>> GetFilteredProductsAsync(PaginationParameters pagination, ProductFilterParameters filter)
        {
            IQueryable<Product> query = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            if (filter.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == filter.CategoryId.Value);

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(p => p.Name.Contains(filter.Name));

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(p => p.Name.Contains(filter.Search));

            var totalCount = await query.CountAsync();


            var pageNumber = Math.Max(1, pagination.PageNumber);
            var pageSize = Math.Clamp(pagination.PageSize, 1, 50);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PagedResult<Product>
            {
                Items = items,
                Metadata = new PaginationMetaData
                {
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    HasNext = pageNumber < totalPages,
                    HasPrevious = pageNumber > 1
                }
            };
        }
    }
}
