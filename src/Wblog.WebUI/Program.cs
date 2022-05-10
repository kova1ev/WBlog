using Wblog.WebUI;
using Wblog.WebUI.Servises;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.AddHttpClient<IBlogClient, BlogClient>();

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

app.UseAuthorization();

app.MapRazorPages();
app.UseBlazorFrameworkFiles("/admin");
app.MapFallbackToFile("/admin/{*path:nonfile}", "/admin/index.html");
app.Run();
