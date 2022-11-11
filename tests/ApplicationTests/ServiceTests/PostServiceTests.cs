using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBlog.Core;
using WBlog.Core.Domain.Entity;
using WBlog.Core.Interfaces;
using WBlog.Core.Services;
using WBlog.Infrastructure.Data;
using WBlog.Infrastructure.Data.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ApplicationTests.ServiceTests
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

        [Fact]
        public async Task Get_publish_post()
        {
            ArticleRequestOptions options = new ArticleRequestOptions { Publish = true };
            //arrage
            IPostRepository postRepository = new PostRepository(appDbContext);
            ITagRepository tagRepository = new TagRepository(appDbContext);
            ITagService tagService = new TagService(tagRepository);
            IPostService postService = new PostService(postRepository, tagService);

            //var mockPost = new Mock<IPostRepository>();
            //mockPost.Setup(repo => repo.Posts).Returns(appDbContext.Posts);
            //var mockTag = new Mock<ITagService>();
            //IPostService postService = new PostService(mockPost.Object, mockTag.Object);

            //atc
            var result = await postService.GetPostsAsync(options);
            //assert
            Assert.True(result.Data.All(p => p.IsPublished == true));
        }

        [Theory]
        [InlineData("pro")]
        [InlineData("BLA")]
        public async Task Get_post_by_querystring(string query)
        {
            ArticleRequestOptions options = new ArticleRequestOptions { Query = query };

            IPostRepository postRepository = new PostRepository(appDbContext);
            ITagRepository tagRepository = new TagRepository(appDbContext);
            ITagService tagService = new TagService(tagRepository);
            IPostService postService = new PostService(postRepository, tagService);

            //var mockPost = new Mock<IPostRepository>();
            //mockPost.Setup(repo => repo.Posts).Returns(appDbContext.Posts);
            //var mockTag = new Mock<ITagService>();
            //IPostService postService = new PostService(mockPost.Object, mockTag.Object);

            var result = await postService.GetPostsAsync(options);

            Assert.True(result.Data.All(p => p.Title.ToLower().Contains(query)));
        }

    }
}
