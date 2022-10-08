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
    //private readonly IPasswordHasher<IdentityUser> _passwordHasher;
    //private readonly IPasswordValidator<IdentityUser> _passwordValidator;
    private readonly UserManager<IdentityUser> _userManager;
    public UserService(UserManager<IdentityUser> userManager)
    {
        //_passwordValidator = passwordValidator;
        _userManager = userManager;
    }

    public async Task<IdentityUser>? GetUserByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user;
    }
    //TODO validation
    //public async Task<bool> Validation(IdentityUser user,string password)
    //{
    //    //var user = await GetUserByEmail(login.Email);
    //    //if (user == null) return false;
    //    // var result = await _passwordValidator.ValidateAsync(_userManager, user, login.Password);
    //    var result = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
    //    return result == PasswordVerificationResult.Success;
    //}

    public async Task<bool> Validation(Login login)
    {
        var user = await GetUserByEmail(login.Email);
        if (user == null) return false;
        // var result = await _passwordValidator.ValidateAsync(_userManager, user, login.Password);
        var result = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, login.Password);
        return result == PasswordVerificationResult.Success;
    }

}
