using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WBlog.Api.Models;
using WBlog.Core.Interfaces;
using WBlog.Core.Domain;

namespace WBlog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserService userService;

    public AccountController(IUserService service)
    {
        userService = service;
    }

    [HttpPost("/login")]
    public async Task<ActionResult> Login([FromBody] LoginModel loginModel)
    {
        var s = HttpContext.Session;
        try
        {
            //TODO login model?
            Login login = new Login { Email = loginModel.Email, Password = loginModel.Password };
            bool result = await userService.Validation(login);
            if (result == false)
                return BadRequest(new { result = Response.StatusCode, messege = "Invalid password or login" });

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, login.Email!) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "WCook");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
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