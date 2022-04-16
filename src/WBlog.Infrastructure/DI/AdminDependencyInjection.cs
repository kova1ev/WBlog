using Microsoft.Extensions.DependencyInjection;
using WBlog.Application.Core.Interfaces;
using WBlog.Infrastructure.Data.Repository;
using WBlog.Infrastructure.Services;

namespace WBlog.Infrastructure.DI
{
    public static class AdminDependencyInjection
    {
        public static void AddAdminService(this IServiceCollection services)
        {
            services.AddScoped<IAdminService, AdminService>();
        }
        public static void AddAdminRepository(this IServiceCollection services)
        {
            services.AddScoped<IAdminRepository, AdminRepository>();
        }
    }
}
