using Project.Common;

namespace Project.BLL
{
    public interface IProductManager
    {
        //Task<IEnumerable<ProductReadDto>> GetFilteredProductsAsync();
        Task<GeneralResult<ProductReadDto>> GetProductByIdAsync(int id);
        Task<GeneralResult<ProductReadDto>> CreateProductAsync(ProductCreateDto dto);
        Task<GeneralResult<ProductReadDto>> UpdateProductAsync(int id, ProductUpdateDto dto);
        Task<GeneralResult> DeleteProductAsync(int id);

    }
}
