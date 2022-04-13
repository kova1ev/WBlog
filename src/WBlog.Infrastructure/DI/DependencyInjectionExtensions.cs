using Microsoft.Extensions.DependencyInjection;
using WBlog.Application.Core.Interfaces;
using WBlog.Infrastructure.Data.Repository;
using WBlog.Infrastructure.Services;

namespace WBlog.Infrastructure.DI
{
    // вынести в отд сборку
    public static class DependencyInjectionExtensions
    {
        public static void AddAdminService(this IServiceCollection services)
        {
            services.AddScoped<IAdminService, AdminService>();
        }
        public static void AddPostService(this IServiceCollection services)
        {
            services.AddScoped<IPostService, PostService>();
        }
        public static void AddTagService(this IServiceCollection services)
        {
            services.AddScoped<ITagService, TagService>();
        }


        public static void AddPostRepository(this IServiceCollection services)
        {
            services.AddScoped<IPostRepository, PostRepository>();
        }
        public static void AddTagRepository(this IServiceCollection services)
        {
            services.AddScoped<ITagRepository, TagRepository>();
        }
        public static void AddAdminRepository(this IServiceCollection services)
        {
            services.AddScoped<IAdminRepository, AdminRepository>();
        }

    }
}