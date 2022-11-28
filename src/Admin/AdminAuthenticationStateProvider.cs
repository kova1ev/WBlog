using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WBlog.Core.Domain.Entity;
using WBlog.Core.Interfaces;

namespace WBlog.Admin;

internal record UserSession(string Id, string UserName, string Role);

public class AdminAuthenticationStateProvider : AuthenticationStateProvider
{
    private const string ADMIN_SESSION_KEY = "user_session";


    private readonly ProtectedLocalStorage _protectedLocalStorage;

    private readonly IUserService _userService;

    private readonly ClaimsPrincipal _annonymous = new(new ClaimsIdentity());

    private readonly ILogger<AdminAuthenticationStateProvider> _logger;

    public AdminAuthenticationStateProvider(ProtectedLocalStorage LocalStorage, ILogger<AdminAuthenticationStateProvider> logger, IUserService userService)
    {
        _protectedLocalStorage = LocalStorage;
        _logger = logger;
        _userService = userService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userSessionResult = await _protectedLocalStorage.GetAsync<UserSession>(ADMIN_SESSION_KEY);
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

    public async Task UpdateAuthenticationState(IdentityUser? user)
    {
        ClaimsPrincipal claimsPrincipal;
        if (user != null)
        {
            UserSession userSession = CreateUserSession(user);
            await _protectedLocalStorage.SetAsync(ADMIN_SESSION_KEY, userSession);

            claimsPrincipal = GetPrincipal(userSession);
        }
        else
        {
            await _protectedLocalStorage.DeleteAsync(ADMIN_SESSION_KEY);
            claimsPrincipal = _annonymous;
        }
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }


    private UserSession CreateUserSession(IdentityUser user)
    {
        return new UserSession(user.Id, user.UserName, "Administrator");
    }
    private ClaimsPrincipal GetPrincipal(UserSession userSession)
    {
        var list = new List<Claim>
            {
                new Claim(ClaimTypes.Name,userSession.UserName),
                new Claim("UserId",userSession.Id),
                new Claim(ClaimTypes.Role,userSession.Role)
            };
        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(list, CookieAuthenticationDefaults.AuthenticationScheme));

        return claimsPrincipal;
    }

}