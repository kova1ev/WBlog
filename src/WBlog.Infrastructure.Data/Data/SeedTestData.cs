using Microsoft.EntityFrameworkCore;
using WBlog.Application.Domain.Entity;

namespace WBlog.Infrastructure.Data
{

    public static class SeedTestData
    {
        public static void CreatdData(AppDbContext dbcontext)
        {
            if (!dbcontext.Posts.Any())
            {
                Tag code = new() { Name = "Code" };
                Tag cat = new() { Name = "Cat" };
                Tag white = new Tag { Name = "white" };
                Tag android = new Tag { Name = "android" };
                Tag black = new Tag { Name = "black" };
                Tag car = new Tag { Name = "car" };
                var posts = new List<Post>()
                {
                    new Post
                    {
                        Title = "Pro C#",
                        Slug = "pro-c-sharp",
                        Content = "C# типизированый это язык програмирования.",
                        Description = "что-то про шарп",
                        Tags = new List<Tag>()
                            {
                                code,
                                new Tag { Name = "CSharp" }
                            }
                    },
                    new Post
                    {
                        Title = "Профессионалы IT",
                        Slug = "professional-it",
                        Content = "Junior , Middle , Senior",
                        Description = "It level gradation",
                        Tags = new List<Tag>
                            {
                                new Tag { Name = "pro" }
                            }
                    },
                    new Post
                    {
                        Title = "Pro Python",
                        Slug = "pro-python",
                        Content = "Python динамический это язык програмирования.",
                        Description = "что-то про питон",
                        Tags = new List<Tag>()
                        {
                            code,
                            new Tag { Name = "Py" }
                        }
                    },
                    new Post
                    {
                        Title = "Pro C/C++",
                        Slug = "pro-c-cpp",
                        Content = "C/C++ типизированый это язык програмирования.",
                        Description = "что-то про С",
                        Tags = new List<Tag>
                        {
                            new Tag{Name ="cpp"}
                        }
                    },
                     new Post
                    {
                        Title = "Blacks cats",
                        Slug = "blacks-cat",
                        Content = "Content about cat",
                        Description = "Description  about cat",
                        Tags = new List<Tag>
                        {
                            black,
                            cat
                        }
                    },
                     new Post
                    {
                        Title = "Белые коты",
                        Slug = "belie-cats",
                        Content = "Контент про белых котов.",
                        Description = "описание про белых котов",
                        Tags = new List<Tag>
                        {
                            white,
                            cat
                        }
                    },
                     new Post
                    {
                        Title = "Веселая жизнь",
                        Slug = "veseloia-jizn",
                        Content = "контент веселой жизни",
                        Description = "описание веселй жизни",
                        Tags = new List<Tag>
                        {
                            new Tag{Name ="жизнь"}
                        }
                    },
                     new Post
                    {
                        Title = "Телефоны Android",
                        Slug = "telefoni-android",
                        Content = "контент про телефоны",
                        Description = "описание телефов",
                        Tags = new List<Tag>
                        {
                            android,
                            new Tag{Name ="phone"}
                        }
                    },
                     new Post
                    {
                        Title = "Планшеты Samsung",
                        Slug = "plansheti-samsung",
                        Content = "Про планшеты самсунг",
                        Description = "описание планшетов самсунг",
                        Tags = new List<Tag>
                        {
                            android,
                            new Tag{Name ="samsung"}
                        }
                    },
                     new Post
                    {
                        Title = "Blacks Cars",
                        Slug = "blacks-cars",
                        Content = "про белые машины",
                        Description = "контент про черных машин",
                        Tags = new List<Tag>
                        {
                            car,
                            black
                        }
                    },
                    new Post
                    {
                        Title = "Whites Cars",
                        Slug = "whites-cars",
                        Content = "про белые машины",
                        Description = "Контент о белых машинах",
                        Tags = new List<Tag>
                        {
                            car,
                            white
                        }
                    },
                    new Post
                    {
                        Title = "Игры от Blizzard",
                        Slug = "blizard-games",
                        Content = "Описание компании",
                        Description = "Описание игр от Близард",
                        Tags = new List<Tag>
                        {
                            new Tag{Name ="blizzard"}
                        }
                    },
                    new Post
                    {
                        Title = "Истрои компании Valve",
                        Slug = "istoria-kompanii-valve",
                        Content = "Кратко о компании Valve",
                        Description = "Полная история компании",
                        Tags = new List<Tag>
                        {
                            new Tag{Name ="valve"}
                        }
                    },

                };

                dbcontext.Posts.AddRange(posts);
                dbcontext.SaveChanges();
            };

            if (!dbcontext.Admin.Any())
            {
                dbcontext.Admin.Add(new Admin()
                {
                    Id = new Guid("447492f2-23cf-4f3a-9f65-4b4b96a52b0d"),
                    Email = "admin@gmail.com",
                    // получать конфига ?
                    Password = "SSE/9LeJeqCqpyHarVKzBc5lic4b/wsTrrY5uLi1png=", //pasword 12345
                });
                dbcontext.SaveChanges();
            }
        }
    }
}