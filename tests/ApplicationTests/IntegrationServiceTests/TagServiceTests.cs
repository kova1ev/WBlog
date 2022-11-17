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

namespace ApplicationTests.IntegrationServiceTests
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

        // BY NAME
        [Theory]
        [InlineData("code")]
        [InlineData("pro")]
        [InlineData("samsung")]
        [InlineData("CAR")]
        public async Task Get_tag_by_name_Success(string tagName)
        {
            ITagRepository tagRepository = new TagRepository(appDbContext);
            ITagService tagService = new TagService(tagRepository);

            var res = await tagService.GetByNameAsync(tagName);

            Assert.Equal(tagName.ToLower(), res?.Name.ToLower());
            Assert.NotNull(res?.Name);
        }

        [Theory]
        [InlineData("aaaaa")]
        [InlineData("bbbb")]
        [InlineData("CCC")]
        public async Task Get_tag_by_name_Invalid(string tagName)
        {
            ITagRepository tagRepository = new TagRepository(appDbContext);
            ITagService tagService = new TagService(tagRepository);

            var result = await tagService.GetByNameAsync(tagName);

            Assert.Null(result);
        }

        //BY FILTERS
        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(2)]
        [InlineData(20)]
        public async Task Get_filtred_tag_By_page_tags_Success(int limit)
        {
            TagRequestOptions options = new TagRequestOptions { Limit = limit };

            ITagRepository tagRepository = new TagRepository(appDbContext);
            ITagService tagService = new TagService(tagRepository);

            var result = await tagService.GetTagsAsync(options);

            Assert.InRange(result.Data.Count(), 0, limit);
        }

        [Theory]
        [InlineData("CA")]
        [InlineData("c")]
        [InlineData("pr")]
        public async Task Get_filtred_tag_by_query_Success(string queryString)
        {
            TagRequestOptions options = new() { Query = queryString };

            ITagRepository tagRepository = new TagRepository(appDbContext);
            ITagService tagService = new TagService(tagRepository);

            var result = await tagService.GetTagsAsync(options);

            Assert.NotEmpty(result.Data);
            Assert.True(result.Data.All(t => t.Name.Contains(queryString, StringComparison.CurrentCultureIgnoreCase)));
        }

        //BY ID
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

            await Assert.ThrowsAsync<ObjectNotFoundException>(async () => await tagService.GetByIdAsync(wrongId));
        }

        //BY POPULAR
        [Fact]
        public async Task Get_pupular_tag_Success()
        {
            ITagRepository tagRepository = new TagRepository(appDbContext);
            ITagService tagService = new TagService(tagRepository);

            var result = await tagService.GetTagsByPopularityAsync(2);

            Assert.Equal(2, result.Count());
            Assert.True(result.First().Posts.Count > 0);
        }

        [InlineData("asdfg")]
        [InlineData("fdsf")]
        [InlineData("sdfsd")]
        [InlineData("vvvvv")]
        [Theory]
        public void Normalize_name(string name)
        {
            ITagRepository tagRepository = new TagRepository(appDbContext);
            ITagService tagService = new TagService(tagRepository);

            string result = tagService.NormalizeName(name);

            Assert.NotNull(result);
            Assert.Equal(name.ToUpper(), result);
        }

        //SAVE  
        [Fact]
        public async Task Save_tag_Success()
        {
            Tag tag = new Tag { Name = "Usa", NormalizeName = "USA" };

            ITagRepository tagRepository = new TagRepository(appDbContext);
            ITagService tagService = new TagService(tagRepository);

            bool result = await tagService.SaveAsync(tag);
            var addedtag = await tagService.GetByNameAsync("usa");

            Assert.True(result);
            Assert.NotNull(addedtag);
            Assert.Equal(tag.Name, addedtag?.Name);
        }

        [Fact]
        public async Task Save_tag_Invalid()
        {
            Tag tag = new Tag { Name = "pro", NormalizeName = "pro" };

            ITagRepository tagRepository = new TagRepository(appDbContext);
            ITagService tagService = new TagService(tagRepository);

            await Assert.ThrowsAsync<ObjectExistingException>(async () => await tagService.SaveAsync(tag));
        }

        [Fact]
        public async Task Save_tag_Invalid_null()
        {
            ITagRepository tagRepository = new TagRepository(appDbContext);
            ITagService tagService = new TagService(tagRepository);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await tagService.SaveAsync(null!));
        }

        //UPDATE
        [Fact]
        public async Task Update_tag_Success()
        {
            Tag tag = new Tag
            {
                Id = new Guid("6819ed45-e4f7-43f5-b1ae-0b5a9ab70132"),
                Name = "C#",// Name = "CSharp"
            };
            ITagRepository tagRepository = new TagRepository(appDbContext);
            ITagService tagService = new TagService(tagRepository);

            bool result = await tagService.UpdateAsync(tag);
            var updatedTag = await tagService.GetByNameAsync(tag.Name);

            Assert.True(result);
            Assert.Equal(tag.Name, updatedTag?.Name);
        }

        [Fact]
        public async Task Update_tag_Invalid()
        {
            Tag tag = new Tag
            {
                Id = new Guid("6819ed45-e4f7-43f5-b1ae-0b5a9ab70132"),
                Name = "CODE", // Name = "CSharp"
            };
            ITagRepository tagRepository = new TagRepository(appDbContext);
            ITagService tagService = new TagService(tagRepository);

            await Assert.ThrowsAsync<ObjectExistingException>(async () => await tagService.UpdateAsync(tag));
        }

        //DELETE
        [Fact]
        public async Task Delete_tag_Success()
        {
            Guid id = new Guid("b0d34bfc-b7e9-4950-a0be-b1c4bf2622b3");

            ITagRepository tagRepository = new TagRepository(appDbContext);
            ITagService tagService = new TagService(tagRepository);

            var result = await tagService.DeleteAsync(id);

            Assert.True(result);
            await Assert.ThrowsAsync<ObjectNotFoundException>(async () => await tagService.DeleteAsync(id));
        }

        [Fact]
        public async Task Delete_tag_Invalid()
        {
            Guid id = new Guid("b0d34bfc-b7e9-4950-a0be-b1c4bf2622b1");

            ITagRepository tagRepository = new TagRepository(appDbContext);
            ITagService tagService = new TagService(tagRepository);

            await Assert.ThrowsAsync<ObjectNotFoundException>(async () => await tagService.DeleteAsync(id));
        }
    }
}
