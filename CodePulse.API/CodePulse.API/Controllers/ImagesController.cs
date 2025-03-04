using CodePulse.API.Models.Domains;
using CodePulse.API.Models.DTOs;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        //GET: {apiBaseUrl}/api/images
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            var blogImages = await imageRepository.GetAllImagesAsync();
            var blogImageDTOs = blogImages.Select(blogImage => new BlogImageDTO
            {
                Id = blogImage.Id,
                FileName = blogImage.FileName,
                FileExtension = blogImage.FileExtension,
                Title = blogImage.Title,
                Url = blogImage.Url,
                CreatedDate = blogImage.CreatedDate,
            }).ToList();
            return Ok(blogImageDTOs);
        }

        //POST: {apiBaseUrl}/api/images
        [HttpPost]
        public async Task<IActionResult> UploadImage(
            [FromForm] IFormFile file,
            [FromForm] string fileName, 
            [FromForm] string title)
        {
            ValidateFileUpload(file, out var fileExtension);
            if (ModelState.IsValid) {
                var blogImage = new BlogImage
                {
                    FileExtension = fileExtension,
                    FileName = fileName,
                    Title = title,
                    CreatedDate = DateTime.Now,
                };

                blogImage = await imageRepository.UploadImageAsync(file, blogImage);

                var response = new BlogImageDTO
                {
                    Id = blogImage.Id,
                    FileName = blogImage.FileName,
                    FileExtension = blogImage.FileExtension,
                    Title = blogImage.Title,
                    Url = blogImage.Url,
                    CreatedDate = blogImage.CreatedDate,
                };
                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(IFormFile file, out string fileExtension)
        {
            var allowExtensions = ApplicationConstant.allowedImageExtensions;
            fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!allowExtensions.Contains(fileExtension)) {
                ModelState.AddModelError("file", "Unsupported file format");
            }
            if(file.Length > ApplicationConstant.maxImgageSize)
            {
                ModelState.AddModelError("file", "File size can't be more than 10MB");

            }
        }
    }
}
