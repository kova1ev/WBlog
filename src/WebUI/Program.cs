using WBlog.WebUI;
using WBlog.WebUI.Servises;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.Configure<SiteOptions>(builder.Configuration.GetSection("SiteOptions"));

builder.Services.AddHttpClient<IBlogClient, BlogClient>();

// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
// .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
//         options.Events = new CookieAuthenticationEvents
//         {
//             OnRedirectToLogin = redirectOption =>
//             {
//                 redirectOption.HttpContext.Response.StatusCode = 401;
//                 return Task.CompletedTask;
//             }
//         }
//     );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseDeveloperExceptionPage();
    app.UseHsts();
}

//app.UseExceptionHandler("/Error");
app.UseStatusCodePagesWithReExecute("/Error", "?handler=ByStatusCode&code={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// app.UseAuthentication();
// app.UseAuthorization();

app.MapRazorPages();

app.Run();
