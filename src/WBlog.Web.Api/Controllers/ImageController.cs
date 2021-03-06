using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace WBlog.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private IWebHostEnvironment environment;
        public ImageController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        [HttpPost]
        public async Task<JsonResult> UploadImage(IFormFile upload)
        {
            // if (upload.Length <= 0) return null;
            // check if the file is image
            // check if the file is too large
            string resultMessage = string.Empty;
            string url = string.Empty;
            string fileName = string.Empty;
            if (upload != null)
            {
                fileName = await ImageSaveAsync(upload);
                resultMessage = "Image is uploaded successfully";
                url = "/img/" + fileName;
            }
            else
            {
                resultMessage = "Image can't uploded!";
            }
            var result = new
            {
                fileName,
                url,
                resultMessage
            };
            return new JsonResult(result);
        }

        /////////////////////////////////////////
        //todo  создать сервис и убрать метод в сервис

        private async Task<string> ImageSaveAsync(IFormFile upload)
        {
            // todo создавать папrу img + вложеные для каждого поста
            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();
            string dir = Directory.GetCurrentDirectory();
            string rootPath = environment.WebRootPath;
            string path = Path.Combine(dir, rootPath, "img/", fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await upload.CopyToAsync(stream);
            }
            return fileName;
        }

    }
}