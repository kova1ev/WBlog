using Microsoft.Extensions.DependencyInjection;
using WBlog.Core.Interfaces;
using WBlog.Infrastructure.Data.Repository;

namespace WBlog.Infrastructure.Data;

public static class DataServiceCollection
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        //services.AddScoped<IUserRepository, AdminRepository>();
    }
}