using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WBlog.Infrastructure.Data.Identity;

public class UserDbContext : IdentityDbContext<IdentityUser>
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
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        var adminEmail = configuration.GetSection("adminEmail").Value;
        var adminPassword = configuration.GetSection("adminPassword").Value;
        var role = configuration.GetSection("adminRole").Value;
        if (await roleMager.FindByNameAsync(role) == null)
            await roleMager.CreateAsync(new IdentityRole(role));
        if (await userManager.FindByNameAsync(adminEmail) == null)
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

