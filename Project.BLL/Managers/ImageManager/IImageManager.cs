
using Project.Common;

namespace Project.BLL
{
    public interface IImageManager
    {
        Task<GeneralResult<ImageUploadResultDto>> UploadAsync(ImageUploadDto imageUploadDto, string basePath, string? schema, string? host);
        Task<GeneralResult> UpdateProductImageAsync(int id, ImageUploadDto dto, string basePath, string schema, string host);
        Task<GeneralResult> UpdateCategoryImageAsync(int id, ImageUploadDto dto, string basePath, string schema, string host);
    }
}
