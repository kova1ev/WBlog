using Microsoft.AspNetCore.Identity;
using WBlog.Core.Domain;

namespace WBlog.Core.Interfaces;

public interface IUserService
{
    Task<IdentityUser>? GetUserByEmailAsync(string email);
    Task<bool> ValidationAsync(Login login);
}