using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
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
    public UserService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityUser>? GetUserByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user;
    }

    public async Task<bool> ValidationAsync(Login login)
    {
        var user = await GetUserByEmailAsync(login.Email);
        if (user == null)
            return false;

        return await _userManager.CheckPasswordAsync(user, login.Password);
    }

}
