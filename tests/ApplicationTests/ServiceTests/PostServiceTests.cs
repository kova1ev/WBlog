using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBlog.Core;
using WBlog.Core.Interfaces;
using WBlog.Core.Services;

namespace ApplicationTests.ServiceTests
{
    public class PostServiceTests
    {
        private readonly MemoryDbContext memoryDb = new MemoryDbContext();


        [Fact]
        public async Task Get_post()
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
    }
}
