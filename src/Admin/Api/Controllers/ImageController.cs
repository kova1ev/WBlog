using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WBlog.Admin.Controllers;

[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
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
    public async Task<IActionResult> UploadImage(IFormFile file)
    {      
            return BadRequest();
        // if (upload.Length <= 0) return null;
        // check if the file is image
        // check if the file is too large
        string resultMessage = string.Empty;
        string url = string.Empty;
        string fileName = string.Empty;
        if (file != null)
        {
            fileName = await ImageSaveAsync(file);
            resultMessage = "Image is uploaded successfully";
            url = "/img/" + fileName;
        }
        else
        {
            resultMessage = "Image can't uploaded!";
        }

        var result = new
        {
            fileName,
            url,
            resultMessage
        };
        return Ok(new { Url = url });
    }

    /////////////////////////////////////////
    //TODO  создать сервис и убрать метод в сервис

    private async Task<string> ImageSaveAsync(IFormFile upload)
    {      
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