using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WBlog.Shared.Dto;
using WBlog.Application.Core.Interfaces;
using WBlog.Application.Core.Models;

namespace WBlog.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAdminService adminService;
        private readonly string salt;
        public AccountController(IAdminService service, IConfiguration configuration)
        {
            adminService = service;
            salt = configuration.GetValue<string>("Salt");
        }

        [HttpPost("/login")]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login([FromBody] LoginDto logindto)
        {
            try
            {
                //todo login model?
                LoginModel loginmodel = new LoginModel { Email = logindto.Email, Password = logindto.Password };
                bool result = await adminService.Validation(loginmodel, salt);
                if (result == false)
                    return BadRequest(new { result = Response.StatusCode, messege = "Invalid password or login" });

                var claims = new List<Claim> { new Claim(ClaimTypes.Name, loginmodel.Email!) };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "WCookies");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return Ok(new { result = Response.StatusCode, messege = "welcome" });
            }
            catch (Exception e)
            {
                return NotFound(new { errorr = e.Message });
            }

        }

        [Authorize]
        [HttpGet("/logout")]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { result = Response.StatusCode, messege = "goodbye" });
        }
    }

}
