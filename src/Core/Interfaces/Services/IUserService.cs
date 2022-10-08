using Microsoft.AspNetCore.Identity;
using WBlog.Core.Domain;

namespace WBlog.Core.Interfaces;

public interface IUserService
{
    public Task<IdentityUser>? GetUserByEmail(string email);
    // Task<bool> Validation(IdentityUser user, string password);
    Task<bool> Validation(Login login);
}