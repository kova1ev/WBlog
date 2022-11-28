using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace WBlog.Core.Interfaces;

public interface IUserService
{
    Task<List<Claim>> CreateUserClaims(IdentityUser user);
    Task<IdentityUser?> GetUserByEmailAsync(string email);
    Task<IdentityUser?> GetUserByIdASync(string id);
    Task<(bool, IdentityUser?)> ValidationUserPasswordAsync(string email, string password);
}