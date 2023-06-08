using NZwalksDpAPI.Data;
using NZwalksDpAPI.Models.Domain;

namespace NZwalksDpAPI.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationDbContext dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath,"Images",$"{ image.FileName}{image.FileExtension}");

            // upload image to local path 
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            // create the url path
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            // add to database 
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
            
        }
    }
}
