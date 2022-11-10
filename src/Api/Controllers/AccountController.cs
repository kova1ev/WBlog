using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WBlog.Api.Models;
using WBlog.Core.Interfaces;
using WBlog.Core.Domain;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;

namespace WBlog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
public class AccountController : ControllerBase
{
    private readonly IUserService userService;

    public AccountController(IUserService service)
    {
        userService = service;
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] LoginModel loginModel)
    {
        Login login = new Login { Email = loginModel.Email, Password = loginModel.Password };
        bool result = await userService.ValidationAsync(login);
        if (result == false)
            return BadRequest(new { result = Response.StatusCode, message = "Invalid password or login" });

        var claims = new List<Claim>
            {
               new Claim(ClaimTypes.Email,login.Email),
            };
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));
        return Ok(new { result = Response.StatusCode, messege = "welcome" });
    }

    [HttpGet("Logout")]
    public async Task<ActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok(new { result = Response.StatusCode, message = "goodbye" });
    }
}