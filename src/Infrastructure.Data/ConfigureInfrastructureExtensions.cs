using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WBlog.Infrastructure.Data;

public static class ConfigureInfrastructureExtensions
{
    public static IdentityBuilder ConfigureIdentity(this IServiceCollection services)
    {
        return services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
    }

    public static IServiceCollection ConfigureAppDbContext(this IServiceCollection services, string connectionString)
    {
        return services.AddDbContext<AppDbContext>(options =>
         {
             options.UseSqlServer(connectionString);
         });
    }
}
