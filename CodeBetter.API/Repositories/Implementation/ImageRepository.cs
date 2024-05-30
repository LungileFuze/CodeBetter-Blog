using CodeBetter.API.Data;
using CodeBetter.API.Models.Domain;
using CodeBetter.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodeBetter.API.Repositories.Implementation
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly CodeBetterDBContext codeBetterDBContext;

        public ImageRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            CodeBetterDBContext codeBetterDBContext) 
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.codeBetterDBContext = codeBetterDBContext;
        }

        public async Task<IEnumerable<BlogImage>> GetAll()
        {
           return await codeBetterDBContext.BlogImages.ToListAsync();
        }

        public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
        {
            //1 - Upload the Image to API/Images
            var localPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{blogImage.FileName}{blogImage.FileExtension}");
            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);
            
            //2 - Update the database
            //https://codebetter.com/images/somefilename.jpg
            var httpRequest = httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{blogImage.FileName}{blogImage.FileExtension}";
            
            blogImage.Url = urlPath;

            await codeBetterDBContext.BlogImages.AddAsync(blogImage);
            await codeBetterDBContext.SaveChangesAsync();

            return blogImage;
        }
    }
}
