using CodePulse.API.Data;
using CodePulse.API.Models.Domains;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ApplicationDbContext dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, ApplicationDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<BlogImage>> GetAllImagesAsync()
        {
            return await dbContext.BlogImages.ToListAsync();
        }

        public async Task<BlogImage> UploadImageAsync(IFormFile file, BlogImage blogImage)
        {
            //Upload Image to API server
            var localPath = Path.Combine(
                webHostEnvironment.ContentRootPath, 
                "Images", 
                $"{blogImage.FileName}{blogImage.FileExtension}");
            
            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);

            //Update database
            var urlPath = $"/images/{blogImage.FileName}{blogImage.FileExtension}";
            blogImage.Url = urlPath;
            
            await dbContext.BlogImages.AddAsync(blogImage);
            await dbContext.SaveChangesAsync();
            return blogImage;
        }
    }
}
