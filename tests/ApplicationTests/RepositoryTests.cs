using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBlog.Core.Domain.Entity;
using WBlog.Core.Interfaces;
using WBlog.Infrastructure.Data;
using WBlog.Infrastructure.Data.Repository;

namespace ApplicationTests
{
    public static class SeedDataForTests
    {

        public static void Seed(AppDbContext dbContext)
        {
            if (dbContext.Posts.Any() == false)
            {
                Tag code = new()
                {
                    Id = new Guid("0ac7974e-9451-4c07-a9b8-ac71f1cbf44e"),
                    Name = "Code",
                    NormalizeName = "Code".ToUpper()
                };
                Tag cat = new()
                {
                    Id = new Guid("dfa877de-d705-4978-be65-7fc3ca289611"),
                    Name = "Cat",
                    NormalizeName = "Cat".ToUpper()
                };
                Tag white = new Tag
                {
                    Id = new Guid("10115049-61e7-4a3a-9e20-7b6ae65311ec"),
                    Name = "white",
                    NormalizeName = "white".ToUpper()
                };
                Tag android = new Tag
                {
                    Id = new Guid("cf792e1b-fd60-40e5-b618-1f2a2e4915c5"),
                    Name = "android",
                    NormalizeName = "android".ToUpper()
                };
                Tag black = new Tag
                {
                    Id = new Guid("93114f4d-8b0e-4dfa-b391-3a79b0d7e2c4"),
                    Name = "black",
                    NormalizeName = "black".ToUpper()
                };
                Tag car = new Tag
                {
                    Id = new Guid("975c0def-c2a5-4f28-88f1-f81facf4b726"),
                    Name = "car",
                    NormalizeName = "car".ToUpper()
                };
                Tag csharp = new Tag
                {
                    Id = new Guid("6819ed45-e4f7-43f5-b1ae-0b5a9ab70132"),
                    Name = "CSharp",
                    NormalizeName = "CSharp".ToUpper()
                };
                Tag pro = new Tag
                {
                    Id = new Guid("4188098d-3d8e-4970-b214-20a350e3049e"),
                    Name = "pro",
                    NormalizeName = "pro".ToUpper()
                };
                Tag py = new Tag
                {
                    Id = new Guid("b0d34bfc-b7e9-4950-a0be-b1c4bf2622b3"),
                    Name = "Py",
                    NormalizeName = "Py".ToUpper()
                };
                Tag cpp = new Tag
                {
                    Id = new Guid("72316442-0a56-4f86-8d14-b85a94d4121d"),
                    Name = "cpp",
                    NormalizeName = "cpp".ToUpper()
                };
                Tag jizn = new Tag
                {
                    Id = new Guid("8b0b0b68-56cb-49dd-bd96-e57f2a7d86d9"),
                    Name = "жизнь",
                    NormalizeName = "жизнь".ToUpper()
                };
                Tag phone = new Tag
                {
                    Id = new Guid("3c534da0-3bca-485d-a48a-9acc79d79aaf"),
                    Name = "phone",
                    NormalizeName = "phone".ToUpper()
                };
                Tag samsung = new Tag
                {
                    Id = new Guid("9a6f54ba-64d1-4879-838d-47b3360ba4ff"),
                    Name = "samsung",
                    NormalizeName = "samsung".ToUpper()
                };
                Tag valve = new Tag
                {
                    Id = new Guid("133f8f7f-6105-44b1-980d-683a22abb897"),
                    Name = "valve",
                    NormalizeName = "valve".ToUpper()
                };
                Tag blizzard = new Tag
                {
                    Id = new Guid("7c6ab66c-fc98-46c1-a367-ee093aa26544"),
                    Name = "blizzard",
                    NormalizeName = "blizzard".ToUpper()
                };


                var posts = new List<Post>()
            {
                new Post
                {
                    Id = new Guid("7c5bf40f-2517-4acf-8c59-357bad010dd8"),
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
                    Id = new Guid("e854e529-1117-4098-b223-81167b6bf2f5"),
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
                    Id = new Guid("5212c9e3-ce2a-413f-bf9e-769e4a8d7d95"),
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
                    Id = new Guid("a8439332-2e27-408c-b375-563481e598c3"),
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
                    Id = new Guid("44ef512f-1795-44ca-b6f1-477ccda45a89"),
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
                    Id = new Guid("f8a1d724-997f-4440-98dd-f45d4e7b4f38"),
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
                    Id = new Guid("4360d800-7eb3-46c9-ba47-1c25a1cc27dc"),
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
                    Id = new Guid("2b87b1a0-64f7-48f1-b55f-d6b3a93a4501"),
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
                    Id = new Guid("89888404-64d9-49e1-9e78-3f4bc6dd5ae3"),
                    Title = "Планшеты Samsung",
                    Slug = "plansheti-samsung",
                       NormalizeSlug = "plansheti-samsung".ToUpper(),
                    Content = "Про планшеты самсунг",
                    Description = "описание планшетов самсунг",
                    IsPublished=true,
                    Tags = new List<Tag>
                    {
                        android,
                        samsung
                    }
                },
                new Post
                {
                    Id = new Guid("3caec385-6039-41aa-94ec-8dda6523e959"),
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
                    Id = new Guid("1cb37f9c-81f8-42fe-badb-45d58818cd81"),
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
                    Id = new Guid("930a33f9-d6f6-45db-80aa-5667a6ddd5d9"),
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
                    Id = new Guid("13eac327-8014-47b9-a29f-58f57abfe122"),
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

                dbContext.Posts.AddRange(posts);
                dbContext.SaveChanges();
            };
        }
    }
}
