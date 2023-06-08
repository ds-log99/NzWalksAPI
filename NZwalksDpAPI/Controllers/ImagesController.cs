using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalksDpAPI.Models.Domain;
using NZwalksDpAPI.Models.DTO;
using NZwalksDpAPI.Repositories;

namespace NZwalksDpAPI.Controllers
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
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadDto request )
        {
            VlidateFileUpload(request);

            if (ModelState.IsValid)
            {
                //convert Dto to domain model 
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,

                };

                await imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);
                
            }

            return BadRequest(ModelState);
        }
        private void VlidateFileUpload(ImageUploadDto request)
        {
            var allowedExtensions = new string[]{".jpg", ".jpeg", ".png"};

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("File", "unssuported file extension");
            }

            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "file size bigger than 10Mb");
            }
        }
    }
}
