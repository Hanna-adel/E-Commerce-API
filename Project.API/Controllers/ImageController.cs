using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BLL;
using Project.Common;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ImageController : ControllerBase
    {
        private readonly IImageManager _imageManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImageController(IImageManager imageManager, IWebHostEnvironment webHostEnvironment)
        {
            _imageManager = imageManager;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost]
        [Route("upload")]
        public async Task<ActionResult<GeneralResult<ImageUploadResultDto>>> UploadAsync([FromForm] ImageUploadDto imageUploadDto)
        {
            var schema = Request.Scheme;
            var host = Request.Host.Value;
            var basePath = _webHostEnvironment.ContentRootPath;

            var result = await _imageManager.UploadAsync(imageUploadDto, basePath, schema, host);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("/api/products/{id}/image")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<GeneralResult>> UpdateProductImageAsync(int id, [FromForm] ImageUploadDto imageUploadDto)
        {
            var schema = Request.Scheme;
            var host = Request.Host.Value;
            var basePath = _webHostEnvironment.ContentRootPath;

            var result = await _imageManager.UpdateProductImageAsync(id, imageUploadDto, basePath, schema, host);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost]
        [Route("/api/categories/{id}/image")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<GeneralResult>> UpdateCategoryImageAsync(int id, [FromForm] ImageUploadDto imageUploadDto)
        {
            var schema = Request.Scheme;
            var host = Request.Host.Value;
            var basePath = _webHostEnvironment.ContentRootPath;

            var result = await _imageManager.UpdateCategoryImageAsync(id, imageUploadDto, basePath, schema, host);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
