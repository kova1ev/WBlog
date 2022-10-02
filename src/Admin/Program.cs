using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using WBlog.Infrastructure.Data;
using WBlog.Shared;
using Microsoft.EntityFrameworkCore;
using Radzen;
using WBlog.Infrastructure.Data.Identity;
using WBlog.Admin.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCoreServices();
builder.Services.AddRepositories();

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("Default"));



//builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<UserDbContext>(); ;
//builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFraemworkStore<UserDbContext>();
//builder.Services.AddScoped<AuthenticationStateProvider>();


builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();


//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options => options.LoginPath = "/Account/Login");
builder.Services.AddAuthorization();

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddServerSideBlazor();

builder.Services.AddAuthenticationCore();
builder.Services.AddSingleton<UserService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

var db = app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
SeedTestData.CreateData(db);

app.Run();