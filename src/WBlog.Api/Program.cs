using WBlog.Core.Repository.Interface;
using WBlog.Core.Repository;
using WBlog.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(js_options =>
{
    js_options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    js_options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    js_options.JsonSerializerOptions.MaxDepth = 0;
});
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
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

app.MapControllers();

SeedTestData.CreatdData(app);
app.Run();
