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

namespace ApplicationTests.ServiceTests
{
    public class TagServiceTests
    {
        private readonly MemoryDbContext memoryDb = new MemoryDbContext();

        [Theory]
        [InlineData("code")]
        [InlineData("pro")]
        [InlineData("samsung")]
        [InlineData("CAR")]
        public async Task Get_tag_by_name_Success(string tagName)
        {
            // arrage
            var mock = new Mock<ITagRepository>();
            mock.Setup(repo => repo.GetByName(tagName).Result).Returns(memoryDb.Tags.FirstOrDefault(t => t.Name.ToLower() == tagName.ToLower()));
            var tagService = new TagService(mock.Object);
            //act
            var res = await tagService.GetByName(tagName);

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
            var mock = new Mock<ITagRepository>();
            mock.Setup(repo => repo.GetByName(tagName).Result).Returns(memoryDb.Tags.FirstOrDefault(t => t.Name.ToLower() == tagName.ToLower()));

            ITagService tagService = new TagService(mock.Object);

            var result = await tagService.GetByName(string.Empty);

            Assert.Null(result);
        }

        [Fact]
        public async Task Get_pupular_tag_Success()
        {
            //arrage
            var mock = new Mock<ITagRepository>();
            mock.Setup(repo => repo.Tags).Returns(memoryDb.Tags);

            ITagService tagService = new TagService(mock.Object);
            //act
            var result = await tagService.GetTagsByPopularity(2);

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
            var mock = new Mock<ITagRepository>();
            mock.Setup(repo => repo.Tags).Returns(memoryDb.Tags);
            ITagService tagService = new TagService(mock.Object);
            //act
            var result = await tagService.GetTags(options);

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
            var mock = new Mock<ITagRepository>();
            mock.Setup(repo => repo.Tags).Returns(memoryDb.Tags);
            ITagService tagService = new TagService(mock.Object);
            //act
            var result = await tagService.GetTags(options);
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
            var mock = new Mock<ITagRepository>();
            mock.Setup(repo => repo.GetById(id).Result).Returns(memoryDb.Tags.FirstOrDefault(t => t.Id == id));
            ITagService tagService = new TagService(mock.Object);

            var result = await tagService.GetById(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task Get_tag_by_id_Invalid()
        {
            Guid id = new Guid("9a6f54ba-64d1-4879-838d-47b3360ba4ff");
            Guid wrongId = Guid.NewGuid();

            var mock = new Mock<ITagRepository>();
            mock.Setup(repo => repo.GetById(id).Result).Returns(memoryDb.Tags.FirstOrDefault(t => t.Id == id));
            ITagService tagService = new TagService(mock.Object);

            await Assert.ThrowsAsync<ObjectNotFoundExeption>(async () => await tagService.GetById(wrongId));
        }
    }
}
