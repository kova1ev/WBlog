using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WBlog.Core;
using WBlog.Core.Domain.Entity;

namespace WBlog.Infrastructure.Data;

public static class SeedTestData
{
    public static void CreateData(IServiceProvider provider)
    {
        var dbcontext = provider.GetRequiredService<AppDbContext>();
        var configuration = provider.GetRequiredService<IConfiguration>();
        var DataBaseProvider = configuration.GetSection("DataBaseProvider").Value;
        if (string.IsNullOrWhiteSpace(DataBaseProvider))
            throw new ArgumentException($"DataBaseProvider from appsettings.json not fount or empty");

        bool result = Enum.TryParse(DataBaseProvider, true, out SupportedDatabaseProvider dbprovider);
        if (result == false)
            throw new ArgumentException("DataBaseProvider has unsupported type");
        if (dbprovider == SupportedDatabaseProvider.InMemory)
        {
            dbcontext.Database.EnsureCreated();
        }

        if (!dbcontext.Posts.Any())
        {
            Tag code = new Tag { Name = "Code", NormalizeName = "Code".ToUpper() };
            Tag cat = new Tag { Name = "Cat", NormalizeName = "Cat".ToUpper() };
            Tag white = new Tag { Name = "white", NormalizeName = "white".ToUpper() };
            Tag android = new Tag { Name = "android", NormalizeName = "android".ToUpper() };
            Tag black = new Tag { Name = "black", NormalizeName = "black".ToUpper() };
            Tag car = new Tag { Name = "car", NormalizeName = "car".ToUpper() };
            Tag csharp = new Tag { Name = "CSharp", NormalizeName = "CSharp".ToUpper() };
            Tag pro = new Tag { Name = "pro", NormalizeName = "pro".ToUpper() };
            Tag py = new Tag { Name = "Py", NormalizeName = "Py".ToUpper() };
            Tag cpp = new Tag { Name = "cpp", NormalizeName = "cpp".ToUpper() };
            Tag jizn = new Tag { Name = "жизнь", NormalizeName = "жизнь".ToUpper() };
            Tag phone = new Tag { Name = "phone", NormalizeName = "phone".ToUpper() };
            Tag samsung = new Tag { Name = "samsung", NormalizeName = "samsung".ToUpper() };
            Tag valve = new Tag { Name = "valve", NormalizeName = "valve".ToUpper() };
            Tag blizzard = new Tag { Name = "blizzard", NormalizeName = "blizzard".ToUpper() };


            var posts = new List<Post>()
            {
                new Post
                {
                    Title = "Pro C#",
                    Slug = "pro-c-sharp",
                    NormalizeSlug = "pro-c-sharp".ToUpper(),
                    Content = "C# типизированый это язык програмирования.",
                    Description = "что-то про шарп",
                    IsPublished=true,
                    Tags = new List<Tag>()
                    {
                        code,
                        csharp
                    }
                },
                new Post
                {
                    Title = "Профессионалы Пирожков",
                    Slug = "professional-pirozhkpv",
                    NormalizeSlug = "professional-pirozhkpv".ToUpper(),
                    Content = "Веселые поедатели пирожков и блинчиков",
                    Description = "Пирожки всем!",
                    Tags = new List<Tag>
                    {
                       pro
                    }
                },
                new Post
                {
                    Title = "Pro Python",
                    Slug = "pro-python",
                     NormalizeSlug = "pro-python".ToUpper(),
                    Content = "Python динамический это язык програмирования.",
                    Description = "что-то про питон",
                    Tags = new List<Tag>()
                    {
                        code,
                        py
                    }
                },
                new Post
                {
                    Title = "Pro C/C++",
                    Slug = "pro-c-cpp",
                     NormalizeSlug = "pro-c-cpp".ToUpper(),
                    Content = "C/C++ типизированый это язык програмирования.",
                    Description = "что-то про С",
                    Tags = new List<Tag>
                    {
                        cpp
                    }
                },
                new Post
                {
                    Title = "Blacks cats",
                    Slug = "blacks-cat",
                    NormalizeSlug = "blacks-cat".ToUpper(),
                    Content = "Content about cat",
                    Description = "Description  about cat",
                    IsPublished=true,
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
                    NormalizeSlug = "belie-cats".ToUpper(),
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
                    NormalizeSlug = "veseloia-jizn".ToUpper(),
                    Content = "контент веселой жизни",
                    Description = "описание веселй жизни",
                    IsPublished=true,
                    Tags = new List<Tag>
                    {
                        jizn
                    }
                },
                new Post
                {
                    Title = "Телефоны Android",
                    Slug = "telefoni-android",
                    NormalizeSlug = "telefoni-android".ToUpper(),
                    Content = "контент про телефоны",
                    Description = "описание телефов",
                    IsPublished=true,
                    Tags = new List<Tag>
                    {
                        android,
                        phone
                    }
                },
                new Post
                {
                    Title = "Планшеты Samsung",
                    Slug = "plansheti-samsung",
                    NormalizeSlug = "plansheti-samsung".ToUpper(),
                    Content = "Про планшеты самсунг",
                    Description = "описание планшетов самсунг",
                    Tags = new List<Tag>
                    {
                        android,
                        samsung
                    }
                },
                new Post
                {
                    Title = "Blacks Cars",
                    Slug = "blacks-cars",
                    NormalizeSlug = "blacks-cars".ToUpper(),
                    Content = "про белые машины",
                    Description = "контент про черных машин",
                    IsPublished=true,
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
                    NormalizeSlug = "whites-cars".ToUpper(),
                    Content = "про белые машины",
                    Description = "Контент о белых машинах",
                    IsPublished=true,
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
                    NormalizeSlug = "blizard-games".ToUpper(),
                    Content = "Описание компании",
                    Description = "Описание игр от Близард",
                    IsPublished=true,
                    Tags = new List<Tag>
                    {
                        blizzard
                    }
                },
                new Post
                {
                    Title = "Истрои компании Valve",
                    Slug = "istoria-kompanii-valve",
                    NormalizeSlug = "istoria-kompanii-valve".ToUpper(),
                    Content = "Кратко о компании Valve",
                    Description = "Полная история компании",
                    Tags = new List<Tag>
                    {
                        valve
                    }
                },
            };

            dbcontext.Posts.AddRange(posts);
            dbcontext.SaveChanges();
        };

    }
}