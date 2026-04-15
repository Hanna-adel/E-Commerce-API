using FluentValidation;
using Project.Common;
using Project.DAL;

namespace Project.BLL
{
    public class ProductManager : IProductManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<ProductCreateDto> _productCreateDtoValidator;
        private readonly IValidator<ProductUpdateDto> _productUpdateDtoValidator;
        public ProductManager(IUnitOfWork unitOfWork, IValidator<ProductCreateDto> productCreateDtoValidator, IValidator<ProductUpdateDto> productUpdateDtoValidator)
        {
            _unitOfWork = unitOfWork;
            _productCreateDtoValidator = productCreateDtoValidator;
            _productUpdateDtoValidator = productUpdateDtoValidator;
        }
        public async Task<GeneralResult<ProductReadDto>> CreateProductAsync(ProductCreateDto productCreateDto)
        {
            var validationResult = await _productCreateDtoValidator.ValidateAsync(productCreateDto);
            if(!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => new Error { Message = e.ErrorMessage}).ToList()

                    );
                return GeneralResult<ProductReadDto>.FailResult(errors);
            }
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(productCreateDto.CategoryId);
            if (category == null)
            {
                return GeneralResult<ProductReadDto>.NotFound("Category not found");
            }

            var product = new Product
            {
                Name = productCreateDto.Name,
                Price = productCreateDto.Price,
                Description = productCreateDto.Description,
                ImageUrl = "placeholder.jpg",
                StockQuantity = productCreateDto.StockQuantity,
                CategoryId = productCreateDto.CategoryId,
            };

            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.SaveAsync();

            var result = new ProductReadDto
            {
                Id = product.Id,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = "placeholder.jpg",
                StockQuantity = product.StockQuantity,
                CategoryName = product.Category?.Name,
            };
            return GeneralResult<ProductReadDto>.SuccessResult(result, "Product created successfully");
        }

        public async Task<GeneralResult> DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
            {
                return GeneralResult<GeneralResult>.NotFound("Product not found");
            }
            _unitOfWork.ProductRepository.Delete(product);
            await _unitOfWork.SaveAsync();
            return GeneralResult.SuccessResult("Product deleted successfully");
        }

        public async Task<GeneralResult<ProductReadDto>> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdWithCategoryAsync(id);
            if(product == null)
            {
                return GeneralResult<ProductReadDto>.NotFound("Product not found");
            }

            var productReadDto = new ProductReadDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                ImageUrl = product.ImageUrl,
                IsAvailable = product.StockQuantity > 0,
                CategoryName = product.Category?.Name,
            };
            return GeneralResult<ProductReadDto>.SuccessResult(productReadDto);
        }

        public async Task<GeneralResult<ProductReadDto>> UpdateProductAsync(int id, ProductUpdateDto productUpdateDto)
        {
            var validationResult = await _productUpdateDtoValidator.ValidateAsync(productUpdateDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => new Error { Message = e.ErrorMessage }).ToList()

                    );
                return GeneralResult<ProductReadDto>.FailResult(errors);
            }
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if(product == null)
            {
                return GeneralResult<ProductReadDto>.NotFound("Product not found");
            }

            var productDto = new ProductReadDto
            {
                StockQuantity = product.StockQuantity,
                Price = product.Price,
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name,
            };

            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.SaveAsync();
            return GeneralResult<ProductReadDto>.SuccessResult(productDto, "Product updated successfully");
        }

        public async Task<GeneralResult<IEnumerable<ProductReadDto>>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllWithCategoryAsync();
            var result = products.Select(c => new ProductReadDto
            {
                Id = c.Id,
                Name = c.Name,
                ImageUrl = c.ImageUrl,
                CategoryId = c.Category.Id,
                CategoryName = c.Category?.Name,
                Description = c.Description,
                StockQuantity = c.StockQuantity,
                Price = c.Price,
                IsAvailable = c.IsAvailable,
            });
            return GeneralResult<IEnumerable<ProductReadDto>>.SuccessResult(result, "Categories retrieved successfully");
        }
    }
}
