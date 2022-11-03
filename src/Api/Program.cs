using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using WBlog.Infrastructure.Data;
using WBlog.Api.Mapper;
using WBlog.Api.Filters;
using WBlog.Shared;
using WBlog.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using WBlog.Core.Interfaces;
using WBlog.Infrastructure.Data.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options => options.Filters.Add(typeof(ApiExceptionFilter))).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(
CookieAuthenticationDefaults.AuthenticationScheme, options =>
    options.Events = new CookieAuthenticationEvents
    {
        OnRedirectToLogin = redirectOption =>
        {
            redirectOption.HttpContext.Response.StatusCode = 401;
            return Task.CompletedTask;
        }
    }
    );
builder.Services.AddAuthorization();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddCoreServices();
builder.Services.AddRepositories();
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("TestCorsPolicy", opt => opt
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

///
builder.Services.ConfigureAppDbContext(builder.Configuration.GetConnectionString("Default"));
builder.Services.ConfigureUserDbContext(builder.Configuration.GetConnectionString("Identity"));
builder.Services.ConfigureIdentity();
///
builder.Services.AddAutoMapper(typeof(PostMapperProfile), typeof(TagsMapperProfile));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DefaultModelsExpandDepth(-1);
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Wblog API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseSession();
app.UseCors("TestCorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//test innit data
var serverProvider = app.Services.CreateScope().ServiceProvider;
await SeedAdmin.SeedAdminData(serverProvider);
SeedTestData.CreateData(serverProvider);

app.Run();