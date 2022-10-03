using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;
using WBlog.Core.Domain.Entity;

namespace WBlog.Admin.Service;

public class UserSession
{
    public string? UserName { get; set; }
    public string? Role { get; set; }
}

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
 //todo
    private readonly ProtectedLocalStorage  _protectedLocalStorage;
    private readonly ProtectedSessionStorage _protectedSessionStorage;

    private ClaimsPrincipal _annonymous = new(new ClaimsIdentity());

    private readonly ILogger<CustomAuthenticationStateProvider> _logger;
    public CustomAuthenticationStateProvider(ProtectedSessionStorage protectedSessionStorage, ProtectedLocalStorage local,ILogger<CustomAuthenticationStateProvider> logger)
    {
         _protectedLocalStorage =local;
        _protectedSessionStorage = protectedSessionStorage;
        _logger = logger;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userSessionResult = await _protectedLocalStorage.GetAsync<UserSession>("UserSession");
            var userSession = userSessionResult.Success ? userSessionResult.Value : null;
            if (userSession == null)
                return await Task.FromResult(new AuthenticationState(_annonymous));

            var clims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userSession.UserName),
        new Claim(ClaimTypes.Role,userSession.Role)
        };
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(clims, "WCook"));
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
        catch (Exception e)
        {
            _logger.LogError($"Date: {DateTime.Now}. Error: {e.Message}");
            return await Task.FromResult(new AuthenticationState(_annonymous));
        }
    }

    public async Task UpadeAuthenticationState(UserSession userSession)
    {
        string key = "UserSession";
        ClaimsPrincipal claimsPrincipal;
        if (userSession != null)
        {
            await _protectedLocalStorage.SetAsync(key, userSession);
            var list = new List<Claim>
            {
                new Claim(ClaimTypes.Name,userSession.UserName),
                new Claim(ClaimTypes.Role,userSession.Role)
            };
            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(list));
        }
        else
        {
            await _protectedLocalStorage.DeleteAsync(key);
            claimsPrincipal = _annonymous;
        }
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

}