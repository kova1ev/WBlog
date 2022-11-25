using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WBlog.Core.Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WBlog.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Tag> Tags => Set<Tag>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Tag>().HasIndex(t => t.NormalizeName).IsUnique();
        builder.Entity<Post>().HasIndex(p => p.NormalizeSlug).IsUnique();

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

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
