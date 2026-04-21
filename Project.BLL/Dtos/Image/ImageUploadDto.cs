

using Microsoft.AspNetCore.Http;

namespace Project.BLL
{
    public sealed class ImageUploadDto
    {
        public IFormFile File { get; init; }
    }
}
