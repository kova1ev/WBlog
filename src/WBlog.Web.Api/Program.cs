using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using WBlog.Infrastructure.Data;
using WBlog.Infrastructure.DI;
using WBlog.Web.Api;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.MaxDepth = 0;

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

builder.Services.AddPostService();
builder.Services.AddTagService();
builder.Services.AddAdminService();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //test innit data
    var dbcontext = app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
    WBlog.Infrastructure.Data.SeedTestData.CreatdData(dbcontext);

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wblog API v1");
        c.RoutePrefix = string.Empty;
    });
}
else
{
    app.UseHsts();
}

app.UseStatusCodePages();
app.UseStaticFiles();
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();
