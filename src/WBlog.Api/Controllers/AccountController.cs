using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WBlog.Core.Dto.RequestModel;
using WBlog.Domain.Entity;
using WBlog.Domain.Repository;

namespace WBlog.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AdminRepository repository;
        public AccountController(AdminRepository repository) { this.repository = repository; }

        [HttpPost("/login")]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login([FromBody] LoginModel login)
        {

            Admin? admin = await repository.GetAdmin();
            var hashpassword = repository.CreateHash(login.Password!);
            if (admin == null)
                return BadRequest(new { result = Response.StatusCode, messege = "Not Admin " });

            if (login.Email != admin.Email || hashpassword == admin.Password)
                return BadRequest(new { result = Response.StatusCode, messege = "Email or password invalid" });

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, admin.Email) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "WCookies");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return Ok(new { result = Response.StatusCode, messege = "welcome" });

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
