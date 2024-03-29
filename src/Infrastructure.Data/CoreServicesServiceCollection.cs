﻿using Microsoft.Extensions.DependencyInjection;
using WBlog.Core.Interfaces;
using WBlog.Core.Services;

namespace WBlog.Infrastructure.Data;

public static class CoreServicesServiceCollection
{
    public static void AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IPostService, PostService>();
    }
}