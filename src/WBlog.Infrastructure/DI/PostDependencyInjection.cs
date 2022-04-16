using Microsoft.Extensions.DependencyInjection;
using WBlog.Application.Core.Interfaces;
using WBlog.Infrastructure.Data.Repository;
using WBlog.Infrastructure.Services;

namespace WBlog.Infrastructure.DI
{
    public static class PostDependencyInjection
    {
        public static void AddPostService(this IServiceCollection services)
        {
            services.AddScoped<IPostService, PostService>();
        }
        public static void AddPostRepository(this IServiceCollection services)
        {
            services.AddScoped<IPostRepository, PostRepository>();
        }
    }
}
