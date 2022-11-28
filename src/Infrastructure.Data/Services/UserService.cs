using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WBlog.Core.Domain;
using WBlog.Core.Domain.Entity;
using WBlog.Core.Exceptions;
using WBlog.Core.Interfaces;

namespace WBlog.Infrastructure.Data.Services;

public class UserService : IUserService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public UserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IdentityUser?> GetUserByIdASync(string id)
    {
        return await _userManager.FindByIdAsync(id.ToString());
    }

    public async Task<IdentityUser?> GetUserByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user;
    }

    public async Task<(bool, IdentityUser?)> ValidationUserPasswordAsync(string email, string password)
    {
        IdentityUser? user = await GetUserByEmailAsync(email);
        if (user == null)
            return (false, user);

        return (await _userManager.CheckPasswordAsync(user, password), user);
    }

    public async Task<List<Claim>> CreateUserClaims(IdentityUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        return new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name,user.UserName),
                        new Claim("UserId",user.Id),
                        new Claim(ClaimTypes.Role, roles.ElementAt(0))
                    };
    }
}
