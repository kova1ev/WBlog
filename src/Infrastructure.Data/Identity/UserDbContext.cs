using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WBlog.Infrastructure.Data.Identity;

public class UserDbContext : IdentityDbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}


public static class SeedAdmin
{
    public static async Task SeedAdminData(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleMager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var adminEmail = "admin@mail.com";
        var adminPassword = "!Aa12345";
        var role = "admin";
        if (await roleMager.FindByIdAsync(role) == null)
            await roleMager.CreateAsync(new IdentityRole(role));
        if (await userManager.FindByIdAsync(adminEmail) == null)
        {
            IdentityUser user = new IdentityUser { Email = adminEmail, UserName = adminEmail };
            var result = await userManager.CreateAsync(user, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }      
    }
}

  