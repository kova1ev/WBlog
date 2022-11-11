using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WBlog.Core.Domain.Entity;
using WBlog.Core.Interfaces;
using WBlog.Core.Services;
using Microsoft.EntityFrameworkCore;
using WBlog.Core;
using WBlog.Core.Exceptions;
using WBlog.Infrastructure.Data;
using WBlog.Infrastructure.Data.Repository;

namespace ApplicationTests.ServiceTests
{
    public class TagServiceTests
    {
        private readonly AppDbContext appDbContext;

        public TagServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>();
            options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            appDbContext = new AppDbContext(options.Options);
            appDbContext.Database.EnsureCreated();
            SeedDataForTests.Seed(appDbContext);
        }
        [Theory]
        [InlineData("code")]
        [InlineData("pro")]
        [InlineData("samsung")]
        [InlineData("CAR")]
        public async Task Get_tag_by_name_Success(string tagName)
        {
            // arrage
            ITagRepository tagRepository = new TagRepository(appDbContext);
            ITagService tagService = new TagService(tagRepository);
            //var mock = new Mock<ITagRepository>();
            //mock.Setup(repo => repo.GetByNameAsync(tagName).Result).Returns(appDbContext.Tags.FirstOrDefault(t => t.Name.ToLower() == tagName.ToLower()));
            //var tagService = new TagService(mock.Object);
            //act
            var res = await tagService.GetByNameAsync(tagName);

            //assert
            Assert.Equal(tagName.ToLower(), res.Name.ToLower());
            Assert.NotNull(res.Name);
        }

        [Theory]
        [InlineData("code")]
        [InlineData("pro")]
        [InlineData("samsung")]
        [InlineData("CAR")]
        public async Task Get_tag_by_name_Invalid(string tagName)
        {
            ITagRepository tagRepository = new TagRepository(appDbContext);
            ITagService tagService = new TagService(tagRepository);

            var result = await tagService.GetByNameAsync(string.Empty);

            Assert.Null(result);
        }

        [Fact]
        public async Task Get_pupular_tag_Success()
        {
            //arrage
            ITagRepository tagRepository = new TagRepository(appDbContext);
            ITagService tagService = new TagService(tagRepository);
            //act
            var result = await tagService.GetTagsByPopularityAsync(2);

            //assert
            Assert.Equal(2, result.Count());
            Assert.True(result.First().Posts.Count > 0);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(2)]
        [InlineData(20)]
        public async Task Get_filtred_tags_Success(int limit)
        {
            TagRequestOptions options = new TagRequestOptions { Limit = limit };
            //arrage

            ITagRepository tagRepository = new TagRepository(appDbContext);
            ITagService tagService = new TagService(tagRepository);

            //act
            var result = await tagService.GetTagsAsync(options);

            //assert
            Assert.True(result.Data.Count() <= limit);
        }

        [Theory]
        [InlineData("ca")]
        [InlineData("c")]
        [InlineData("pr")]
        public async Task Get_filtred_tag_by_query_Success(string queryString)
        {
            //arrage
            TagRequestOptions options = new() { Query = queryString };

            ITagRepository tagRepository = new TagRepository(appDbContext);
            ITagService tagService = new TagService(tagRepository);

            //act
            var result = await tagService.GetTagsAsync(options);
            //assert
            Assert.Contains(queryString, result.Data.ElementAt(0).Name.ToLower());
            Assert.Contains(queryString, result.Data.ElementAt(result.Data.Count() - 1).Name.ToLower());
        }

        [Theory]
        [InlineData("9a6f54ba-64d1-4879-838d-47b3360ba4ff")]
        [InlineData("3c534da0-3bca-485d-a48a-9acc79d79aaf")]
        [InlineData("4188098d-3d8e-4970-b214-20a350e3049e")]
        public async Task Get_tag_by_id_Success(Guid id)
        {
            ITagRepository tagRepository = new TagRepository(appDbContext);
            ITagService tagService = new TagService(tagRepository);

            var result = await tagService.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task Get_tag_by_id_Invalid()
        {
            Guid id = new Guid("9a6f54ba-64d1-4879-838d-47b3360ba4ff");
            Guid wrongId = Guid.NewGuid();

            ITagRepository tagRepository = new TagRepository(appDbContext);
            ITagService tagService = new TagService(tagRepository);

            await Assert.ThrowsAsync<ObjectNotFoundExeption>(async () => await tagService.GetByIdAsync(wrongId));
        }
    }
}
