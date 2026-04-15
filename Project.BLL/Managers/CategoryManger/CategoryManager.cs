using FluentValidation;
using Project.Common;
using Project.DAL;


namespace Project.BLL
{
    public class CategoryManager : ICategoryManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CategoryCreateDto> _categoryCreateDtoValidator;
        private readonly IValidator<CategoryUpdateDto> _categoryUpdateDtoValidator;
        public CategoryManager(IUnitOfWork unitOfWork, IValidator<CategoryCreateDto> categoryCreateDtoValidator, IValidator<CategoryUpdateDto> categoryUpdateDtoValidator)
        {
            _unitOfWork = unitOfWork;
            _categoryCreateDtoValidator = categoryCreateDtoValidator;
            _categoryUpdateDtoValidator = categoryUpdateDtoValidator;
        }
        public async Task<GeneralResult<CategoryReadDto>> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            var validationResult = await _categoryCreateDtoValidator.ValidateAsync(categoryCreateDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => new Error { Message = e.ErrorMessage }).ToList()

                    );
                return GeneralResult<CategoryReadDto>.FailResult(errors);
            }
            var category = new Category
            {
                Name = categoryCreateDto.Name,
                ImageUrl = "placeholder.jpg"
            };

            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.SaveAsync();

            var result = new CategoryReadDto
            {
                Id = category.Id,
                Name = category.Name,
                ImageUrl = category.ImageUrl
            };

            return GeneralResult<CategoryReadDto>.SuccessResult(result, "Category created successfully");
        }

        public async Task<GeneralResult> DeleteCategoryAsync(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetByIdAsync(id).Result;
            if (category == null)
            {
                return GeneralResult.NotFound("Category not found");
            }
            _unitOfWork.CategoryRepository.Delete(category);
            await _unitOfWork.SaveAsync();
            return GeneralResult.SuccessResult("Category deleted successfully");

        }

        public async Task<GeneralResult<IEnumerable<CategoryReadDto>>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            var result = categories.Select(c => new CategoryReadDto
            {
                Id = c.Id,
                Name = c.Name,
                ImageUrl = c.ImageUrl

            });
            return GeneralResult<IEnumerable<CategoryReadDto>>.SuccessResult(result, "Categories retrieved successfully");
        }

        public async Task<GeneralResult<CategoryReadDto>> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if(category == null)
            {
                return GeneralResult<CategoryReadDto>.NotFound("Category not found");
            }
            var result = new CategoryReadDto
            {
                Id = category.Id,
                Name = category.Name,
                ImageUrl = category.ImageUrl
            };
            return GeneralResult<CategoryReadDto>.SuccessResult(result, "Category retrieved successfully");
        }

        public async Task<GeneralResult<CategoryReadDto>> UpdateCategoryAsync(int id, CategoryUpdateDto categoryUpdateDto)
        {
            var validationResult = await _categoryUpdateDtoValidator.ValidateAsync(categoryUpdateDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => new Error { Message = e.ErrorMessage }).ToList()

                    );
                return GeneralResult<CategoryReadDto>.FailResult(errors);
            }
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if(category == null)
            {
                return GeneralResult<CategoryReadDto>.NotFound("Category not found");
            }
            category.Name = categoryUpdateDto.Name;

            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.SaveAsync();

            var result = new CategoryReadDto
            {
                Id = category.Id,
                Name = category.Name,
                ImageUrl = category.ImageUrl
            };

            return GeneralResult<CategoryReadDto>.SuccessResult(result, "Category updated successfully");

        }
    }
}
