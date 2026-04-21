
using FluentValidation;
using Project.Common;
using Project.DAL;

namespace Project.BLL
{
    public class ImageManager : IImageManager
    {
        private readonly IValidator<ImageUploadDto> _validator;
        private readonly IErrorMapper _errorMapper;
        private readonly IUnitOfWork _unitOfWork;

        public ImageManager(IValidator<ImageUploadDto> validator, IErrorMapper errorMapper, IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _errorMapper = errorMapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<GeneralResult<ImageUploadResultDto>> UploadAsync(
            ImageUploadDto imageUploadDto,
            string basePath,
            string? schema,
            string? host)
        {
            if (string.IsNullOrWhiteSpace(schema) || string.IsNullOrWhiteSpace(host))
            {
                return GeneralResult<ImageUploadResultDto>.FailRequest("Missing schema or host");
            }
            var result = await _validator.ValidateAsync(imageUploadDto);
            if (!result.IsValid)
            {
                var errors = _errorMapper.MapError(result);
                return GeneralResult<ImageUploadResultDto>.FailResult(errors);
            }

            var file = imageUploadDto.File;
            var extention = Path.GetExtension(file.FileName).ToLower();
            var cleanName = Path.GetFileNameWithoutExtension(file.FileName).Replace(" ", "-").ToLower();
            var newFileName = $"{cleanName}-{Guid.NewGuid()}{extention}";
            var directoryPath = Path.Combine(basePath, "Files");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var fullFilePath = Path.Combine(directoryPath, newFileName);
            using (var stream = new FileStream(fullFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var url = $"{schema}://{host}/Files/{newFileName}";
            var imageUploadResultDto = new ImageUploadResultDto(url);
            return GeneralResult<ImageUploadResultDto>.SuccessResult(imageUploadResultDto);
        }
        public async Task<GeneralResult> UpdateProductImageAsync(int id, ImageUploadDto dto, string basePath, string schema, string host)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
                return GeneralResult.NotFound("Product not found");

            var uploadResult = await UploadAsync(dto, basePath, schema, host);
            if (!uploadResult.Success)
                return GeneralResult.FailRequest("Image upload failed");


            product.ImageUrl = uploadResult.Data!.ImageUrl;
            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.SaveAsync();

            return GeneralResult.SuccessResult("Product image updated successfully");
        }

        public async Task<GeneralResult> UpdateCategoryImageAsync(int id, ImageUploadDto dto, string basePath, string schema, string host)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
                return GeneralResult.NotFound("Category not found");


            var uploadResult = await UploadAsync(dto, basePath, schema, host);
            if (!uploadResult.Success)
                return GeneralResult.FailRequest("Image upload failed");


            category.ImageUrl = uploadResult.Data!.ImageUrl;
            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.SaveAsync();

            return GeneralResult.SuccessResult("Category image updated successfully");
        }
    }
}
