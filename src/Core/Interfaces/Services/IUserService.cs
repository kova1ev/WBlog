using Microsoft.AspNetCore.Identity;
using WBlog.Core.Domain;

namespace WBlog.Core.Interfaces;

public interface IUserService
{
    public IdentityUser? GetUser(string emial);
    Task<bool> Validation(Login login, string salt);
}