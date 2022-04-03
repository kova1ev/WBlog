using Microsoft.EntityFrameworkCore;
using WBlog.Domain.Entity;
using WBlog.Domain.Data;

namespace WBlog.Api
{
    public static class SeedTestData
    {
        public static void CreatdData(IApplicationBuilder app)
        {
            var dbcontext = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
            dbcontext.Database.Migrate();
            if (!dbcontext.Posts.Any())
            {
                Tag tag1 = new()
                {
                    Name = "Code"
                };

                Post post1 = new()
                {
                    Title = "Pro C#",
                    Slug = "pro-c#",
                    Contetnt = "C# типизированый это язык програмирования.",
                    Descriprion = "что-то про шарп",
                    Tags = new List<Tag>()
                    {
                        tag1,
                        new Tag { Name = "CSharp" }
                    }
                };

                Post post2 = new()
                {
                    Title = "Pro Python",
                    Slug = "pro-python",
                    Contetnt = "Python динамический это язык програмирования.",
                    Descriprion = "что-то про питон",
                    Tags = new List<Tag>()
                    {
                        tag1,
                        new Tag { Name = "Py" }
                    }
                };
                Post post3 = new ()
                {
                    Title = "Pro C/C++",
                    Slug ="pro-c-cpp",
                    Contetnt = "C/C++ типизированый это язык програмирования.",
                    Descriprion = "что-то про С",
                };
                Post post4 = new()
                {
                    Title = "Профессионалы IT",
                    Slug = "professional-it",
                    Contetnt = "Junior , Middle , Senior",
                    Descriprion = "It level gradation",
                    Tags = new List<Tag> { new Tag { Name = "pro"} }
                };
                dbcontext.Posts.AddRange(post1, post2, post3, post4);
                dbcontext.SaveChanges();
            }
        }

    }
}
