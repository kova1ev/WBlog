using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WBlog.Core;

namespace WBlog.Infrastructure.Data;

public static class ConfigureInfrastructureExtensions
{
    public static IdentityBuilder ConfigureIdentity(this IServiceCollection services)
    {
        return services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
    }

    public static IServiceCollection ConfigureAppDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<AppDbContext>(options =>
        {
            var DataBaseProvider = configuration.GetSection("DataBaseProvider").Value;
            if (string.IsNullOrWhiteSpace(DataBaseProvider))
                throw new ArgumentException($"DataBaseProvider from appsettings.json not fount or empty");

            bool result = Enum.TryParse(DataBaseProvider, true, out SupportedDatabaseProvider dbprovider);
            if (result == false)
                throw new ArgumentException("DataBaseProvider has unsupported type");

            if (SupportedDatabaseProvider.InMemory == dbprovider)
            {
                options.UseInMemoryDatabase("MyDb");
            }
            else if (SupportedDatabaseProvider.MsSql == dbprovider)
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            }
        });



    }
}
