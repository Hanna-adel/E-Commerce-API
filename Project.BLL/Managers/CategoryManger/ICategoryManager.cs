using Project.Common;

namespace Project.BLL
{
    public interface ICategoryManager
    {
        Task<GeneralResult<IEnumerable<CategoryReadDto>>> GetAllCategoriesAsync();
        Task<GeneralResult<CategoryReadDto>> GetCategoryByIdAsync(int id);
        Task<GeneralResult<CategoryReadDto>> CreateCategoryAsync(CategoryCreateDto categoryCreateDto);
        Task<GeneralResult<CategoryReadDto>> UpdateCategoryAsync(int id, CategoryUpdateDto categoryUpdateDto);
        Task<GeneralResult> DeleteCategoryAsync(int id);
    }
}
