using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WBlog.Core.Domain.Entity;

namespace WBlog.Infrastructure.Data.Identity;

public class UserDbContext : IdentityDbContext
{
    public DbSet<User> Users => Set<User>();
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}

// public static class IdentityCollection
// {
//     public static void AddDefault(this IServiceCollection service)
//     {
//         service.AddDefaultIdentity<IdentityUser>().AddEntityFraemworkStore<UserDbContext>();
//     }
// }