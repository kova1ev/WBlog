using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using WBlog.Core.Interfaces;
using WBlog.Core.Services;
using WBlog.Core;
using WBlog.Infrastructure.Data.Repository;
using WBlog.Infrastructure.Data;
using WBlog.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Collections;
using WBlog.Core.Domain.Entity;
using System.Formats.Asn1;

namespace ApplicationTests.IntegrationServiceTests
{
    public class PostServiceTests
    {
        private readonly AppDbContext appDbContext;

        public PostServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>();
            options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            appDbContext = new AppDbContext(options.Options);
            appDbContext.Database.EnsureCreated();
            SeedDataForTests.Seed(appDbContext);
        }

        private IPostService CreatePostService()
        {
            IPostRepository postRepository = new PostRepository(appDbContext);
            ITagRepository tagRepository = new TagRepository(appDbContext);
            ITagService tagService = new TagService(tagRepository);
            return new PostService(postRepository, tagService);
        }


        //BY ID
        [Theory]
        [InlineData("3caec385-6039-41aa-94ec-8dda6523e959")]
        [InlineData("930a33f9-d6f6-45db-80aa-5667a6ddd5d9")]
        [InlineData("13eac327-8014-47b9-a29f-58f57abfe122")]
        public async Task Get_post_by_id_Success(Guid id)
        {
            IPostService postService = CreatePostService();

            var result = await postService.GetPostByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Theory]
        [InlineData("3caec385-6039-41aa-94ec-8dda6523e951")]
        [InlineData("930a33f9-d6f6-45db-80aa-5667a6ddd5d1")]
        [InlineData("13eac327-8014-47b9-a29f-58f57abfe121")]
        public async Task Get_post_by_id_Invalid(Guid id)
        {
            IPostService postService = CreatePostService();

            await Assert.ThrowsAsync<ObjectNotFoundException>(async () => await postService.GetPostByIdAsync(id));
        }

        //SLUG
        [Theory]
        [InlineData("whites-cars")]
        [InlineData("pro-python")]
        [InlineData("belie-cats")]
        public async Task Get_post_by_slug_Success(string slug)
        {
            IPostService postService = CreatePostService();

            var result = await postService.GetPostBySlugAsync(slug);

            Assert.NotNull(result);
            Assert.Equal(slug, result?.Slug);
        }

        [Theory]
        [InlineData("aaaaa-sssss")]
        [InlineData("vvvvvv-ccc")]
        public async Task Get_post_by_slug_Invalid(string slug)
        {
            IPostService postService = CreatePostService();

            var result = await postService.GetPostBySlugAsync(slug);

            Assert.Null(result);
        }

        [Theory]
        [InlineData("aaaaa-sssss")]
        [InlineData("vvvvvv-ccc")]
        public void Normalize_slug(string slug)
        {
            IPostService postService = CreatePostService();

            var result = postService.NormalizeSlug(slug);

            Assert.NotNull(result);
            Assert.Equal(slug.ToUpper(), result);
        }

        //GET Post Tags
        [Fact]
        public async Task Get_post_tags()
        {
            var id = new Guid("44ef512f-1795-44ca-b6f1-477ccda45a89");
            IPostService postService = CreatePostService();

            var result = await postService.GetPostTagsAsync(id);

            Assert.NotEmpty(result);
            Assert.True(result.Count() == 2);
        }


        //Get
        [Fact]
        public async Task Get_published_posts()
        {
            ArticleRequestOptions options = new ArticleRequestOptions { Publish = true };

            IPostService postService = CreatePostService();

            var result = await postService.GetPostsAsync(options);

            Assert.True(result.Data.All(p => p.IsPublished));
        }

        [Theory]
        [InlineData("cat")]
        [InlineData("white")]
        [InlineData("valve")]
        public async Task Get_post_by_tag(string tag)
        {
            ArticleRequestOptions options = new ArticleRequestOptions { Tag = tag };

            IPostService postService = CreatePostService();

            var result = await postService.GetPostsAsync(options);

            var normalizeName = options.Tag.ToUpper();
            Assert.True(result.Data.All(p => p.Tags.All(p => p.NormalizeName == normalizeName)));
        }

        [Theory]
        [InlineData("cat")]
        [InlineData("whi")]
        [InlineData("планш")]
        public async Task Get_post_by_query(string query)
        {
            ArticleRequestOptions options = new ArticleRequestOptions { Query = query };

            IPostService postService = CreatePostService();

            var result = await postService.GetPostsAsync(options);

            Assert.NotEmpty(result.Data);
            Assert.True(result.Data.All(t => t.Title.Contains(query, StringComparison.CurrentCultureIgnoreCase)));
        }

        //SAVE
        [Fact]
        public async Task Save_post_Success()
        {
            Post post = new Post
            {
                Title = "History of Europe",
                Slug = "history-of-europe",
                Content = "Content  History of Europe",
                Description = "Description History of Europe",
                IsPublished = true,
                Tags = new List<Tag>
                    {
                        new Tag{ Name="book"},
                        new Tag { Name="history"}
                    }
            };

            IPostService postService = CreatePostService();

            var result = await postService.SaveAsync(post);
            var addedPost = await postService.GetPostBySlugAsync(post.Slug);

            Assert.True(result);
            Assert.NotNull(addedPost);
            Assert.Equal(post.Slug, addedPost?.Slug);
        }


        [Fact]
        public async Task Save_post_Invalid()
        {
            Post post = new Post
            {
                Title = "History of Europe",
                Slug = "blacks-cat",
                Content = "Content  History of Europe",
                Description = "Description History of Europe",
                IsPublished = true,
                Tags = new List<Tag>
                    {
                        new Tag{ Name="book"},
                        new Tag { Name="history"}
                    }
            };
            IPostService postService = CreatePostService();
            await Assert.ThrowsAsync<ObjectExistingException>(async () => await postService.SaveAsync(post));
        }

        // UPDATE
        [Fact]
        public async Task Update_post_Success()
        {
            IPostService postService = CreatePostService();

            var post = await postService.GetPostBySlugAsync("pro-c-sharp");
            post!.Slug = "pro-Csharp";
            var result = await postService.UpdateAsync(post);

            var checkPost = await postService.GetPostBySlugAsync("pro-Csharp");


            Assert.True(result);
            Assert.NotNull(checkPost);
            Assert.Equal(post.Id, checkPost?.Id);
        }

        [Fact]
        public async Task Update_post_Invalid()
        {
            IPostService postService = CreatePostService();

            var post = await postService.GetPostBySlugAsync("pro-c-sharp");
            post!.Slug = "blacks-cat";

            await Assert.ThrowsAsync<ObjectExistingException>(async () => await postService.UpdateAsync(post));
        }

        //DELETE
        [Fact]
        public async Task Delete_post_Success()
        {
            string slug = "blacks-cars";
            IPostService postService = CreatePostService();
            var post = await postService.GetPostBySlugAsync(slug);

            var result = await postService.DeleteAsync(post.Id);

            var checkPost = await postService.GetPostBySlugAsync(slug);
            Assert.True(result);
            Assert.Null(checkPost);
        }

        [Fact]
        public async Task Delete_post_Invalid()
        {
            Guid invalidId = Guid.NewGuid();
            IPostService postService = CreatePostService();

            await Assert.ThrowsAsync<ObjectNotFoundException>(async () => await postService.DeleteAsync(invalidId));
        }
    }
}
