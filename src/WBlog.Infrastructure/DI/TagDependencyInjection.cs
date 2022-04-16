using Microsoft.Extensions.DependencyInjection;
using WBlog.Application.Core.Interfaces;
using WBlog.Infrastructure.Data.Repository;
using WBlog.Infrastructure.Services;

namespace WBlog.Infrastructure.DI
{
    public static class TagDependencyInjection
    {
        public static void AddTagService(this IServiceCollection services)
        {
            services.AddScoped<ITagService, TagService>();
        }
        public static void AddTagRepository(this IServiceCollection services)
        {
            services.AddScoped<ITagRepository, TagRepository>();
        }
    }
}
