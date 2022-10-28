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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ApplicationTests.ServiceTests
{
    public class PostServiceTests
    {
        private readonly MemoryDbContext memoryDb = new MemoryDbContext();


        [Fact]
        public async Task Get_publish_post()
        {
            ArticleRequestOptions options = new ArticleRequestOptions { Publish = true };
            //arrage
            var mockPost = new Mock<IPostRepository>();
            mockPost.Setup(repo => repo.Posts).Returns(memoryDb.Posts);
            var mockTag = new Mock<ITagRepository>();
            mockTag.Setup(repo => repo.Tags).Returns(memoryDb.Tags);
            IPostService postService = new PostService(mockPost.Object, mockTag.Object);
            //atc
            var result = await postService.GetPosts(options);
            //assert
            Assert.True(result.Data.All(p => p.IsPublished == true));
        }

        [Theory]
        [InlineData("pro")]
        [InlineData("BLA")]
        public async Task Get_post_by_querystring(string query)
        {
            ArticleRequestOptions options = new ArticleRequestOptions { Query = query };

            var mockPost = new Mock<IPostRepository>();
            mockPost.Setup(repo => repo.Posts).Returns(memoryDb.Posts);
            var mockTag = new Mock<ITagRepository>();
            IPostService postService = new PostService(mockPost.Object, mockTag.Object);

            var result = await postService.GetPosts(options);

            Assert.True(result.Data.All(p => p.Title.ToLower().Contains(query.ToLower())));
        }


    }
}
