using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using WBlog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Radzen;
using WBlog.Infrastructure.Data.Services;
using WBlog.Core.Interfaces;
using WBlog.Admin.Mapper;
using System.Text.Json.Serialization;
using WBlog.Admin.Filters;
using WBlog.Admin;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCoreServices();
builder.Services.AddRepositories();
builder.Services.AddScoped<IUserService, UserService>();

//add Radzen services
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

//
builder.Services.ConfigureAppDbContext(builder.Configuration);
builder.Services.ConfigureIdentity();

//mapper
builder.Services.AddAutoMapper(typeof(PostMapperProfile), typeof(TagsMapperProfile));

//defaults
builder.Services.AddControllers(options => options.Filters.Add(typeof(ApiExceptionFilter))).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddServerSideBlazor();

builder.Services.AddAuthorization();

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

builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddScoped<AuthenticationStateProvider, AdminAuthenticationStateProvider>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("WblogPolicy", opt => opt
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.DefaultModelsExpandDepth(-1);
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Wblog API v1");
    options.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();
app.UseCors("WblogPolicy");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapControllers();

var serverProvider = app.Services.CreateScope().ServiceProvider;
SeedTestData.CreateData(serverProvider);
await SeedAdmin.SeedAdminData(serverProvider);


app.Run();