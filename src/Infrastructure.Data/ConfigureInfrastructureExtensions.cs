using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WBlog.Infrastructure.Data.Identity;

namespace WBlog.Infrastructure.Data;

public static class ConfigureInfrastructureExtensions
{
    public static IdentityBuilder ConfigureIdentity(this IServiceCollection services)
    => services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<UserDbContext>();

    public static IServiceCollection ConfigureAppDbContext(this IServiceCollection services, string connectionString)

         => services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });


    public static IServiceCollection ConfigureUserDbContext(this IServiceCollection services, string connectionString)
     => services.AddDbContext<UserDbContext>(options => options.UseSqlServer(connectionString));

}
