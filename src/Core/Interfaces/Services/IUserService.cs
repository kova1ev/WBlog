using Microsoft.AspNetCore.Identity;
using WBlog.Core.Domain;

namespace WBlog.Core.Interfaces;

public interface IUserService
{
    Task<IdentityUser>? GetUserByEmail(string email);
    Task<bool> Validation(Login login);
}