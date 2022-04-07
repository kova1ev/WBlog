using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WBlog.Core.Dto.RequestModel;

namespace WBlog.Api.Controllers
{
    record Admin(string Email, string Password);

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        Admin admin = new("admin@gmail.com", "12345");
        public AccountController() { }

        [HttpPost("/login")]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login([FromBody] LoginModel login)
        {
            if (login.Email != admin.Email || login.Password != admin.Password)
                return BadRequest(new { result = Response.StatusCode, messege = "Email or password invalid" });

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, admin.Email) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "WCookies");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return Ok(new { result = Response.StatusCode, messege = "welcome" });

        }

        [HttpGet("/logout")]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { result = Response.StatusCode, messege = "goodbye" });
        }
    }

}
