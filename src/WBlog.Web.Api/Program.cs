using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using WBlog.Infrastructure.Data;
using WBlog.Infrastructure.DI;
using WBlog.Web.Api.Mapper;
using WBlog.Web.Api.Filters;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options => options.Filters.Add(typeof(ApiExceptionFilter))).AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
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

builder.Services.AddPostRepository();
builder.Services.AddTagRepository();
builder.Services.AddAdminRepository();

builder.Services.AddPostService();
builder.Services.AddTagService();
builder.Services.AddAdminService();

//builder.Services.AddDbContext<AppDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
//});

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("memorydb"));

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//test innit data
var dbcontext = app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
WBlog.Infrastructure.Data.SeedTestData.CreatdData(dbcontext);

app.Run();
