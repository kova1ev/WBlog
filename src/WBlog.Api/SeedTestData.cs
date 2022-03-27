using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBlog.Domain.Entity;

namespace WBlog.Core.Data
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
                dbcontext.Posts.AddRange(post1, post2, post3);
                dbcontext.SaveChanges();
            }
        }

    }
}
