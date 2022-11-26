using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WBlog.Core.Domain.Entity;

namespace WBlog.Admin.Service;

public class UserSession
{
    public string? UserName { get; set; }
    public string? UserEmail { get; set; }
    public string Role { get; set; } = "Administrator";

    public static UserSession CreateUserSession(IdentityUser user)
    {
        return new UserSession
        {
            UserName = user.UserName,
            UserEmail = user.Email,
        };
    }
}

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    //todo

    private readonly ProtectedLocalStorage _protectedLocalStorage;

    private ClaimsPrincipal _annonymous = new(new ClaimsIdentity());

    private readonly ILogger<CustomAuthenticationStateProvider> _logger;
    public CustomAuthenticationStateProvider(ProtectedLocalStorage local, ILogger<CustomAuthenticationStateProvider> logger)
    {
        _protectedLocalStorage = local;
        _logger = logger;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userSessionResult = await _protectedLocalStorage.GetAsync<UserSession>(nameof(UserSession));
            var userSession = userSessionResult.Success ? userSessionResult.Value : null;
            if (userSession == null)
                return await Task.FromResult(new AuthenticationState(_annonymous));

            var claimsPrincipal = GetPrincipal(userSession);
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
        catch (Exception e)
        {
            _logger.LogError($"Date: {DateTime.Now}. Error: {e.Message}");
            return await Task.FromResult(new AuthenticationState(_annonymous));
        }
    }

    public async Task UpdateAuthenticationState(UserSession? userSession)
    {
        string key = nameof(UserSession);
        ClaimsPrincipal claimsPrincipal;
        if (userSession != null)
        {
            await _protectedLocalStorage.SetAsync(key, userSession);
            claimsPrincipal = GetPrincipal(userSession);
        }
        else
        {
            await _protectedLocalStorage.DeleteAsync(key);
            claimsPrincipal = _annonymous;
        }
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    private ClaimsPrincipal GetPrincipal(UserSession userSession)
    {
        var list = new List<Claim>
            {
                new Claim(ClaimTypes.Name,userSession.UserName),
                new Claim(ClaimTypes.Email,userSession.UserEmail),
                new Claim(ClaimTypes.Role,userSession.Role)
            };
        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(list, CookieAuthenticationDefaults.AuthenticationScheme));

        return claimsPrincipal;
    }

}