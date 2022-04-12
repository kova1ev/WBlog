using WBlog.Application.Core.Services;
using WBlog.Application.Domain.Entity;
using WBlog.Application.Core.Dto;
using Microsoft.EntityFrameworkCore;

namespace WBlog.Infrastructure.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository tagRepository;
        public TagService(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public async Task<IEnumerable<TagDto>> GetAllTags()
        {
            return await tagRepository.Tags.Select(t => new TagDto { Id = t.Id, Name = t.Name }).ToListAsync();
        }

        public async Task<IEnumerable<PopularTagDto>> GetTagsByPopularity()
        {

            var tagsList = await tagRepository.Tags.Include(t => t.Posts)
                                    .OrderByDescending(t => t.Posts.Count())
                                    .ToListAsync();
            return tagsList.Select(t => new PopularTagDto { Id = t.Id, Name = t.Name, PostCount = t.Posts.Count() });
        }

        public async Task<TagDto?> GetById(Guid id)
        {
            var tag = await tagRepository.GetById(id);
            if (tag != null)
                return new TagDto { Id = tag.Id, Name = tag.Name };
            return null;
        }

        public async Task<TagDto?> GetByName(string name)
        {
            var tag = await tagRepository.GetByName(name);
            if (tag != null)
                return new TagDto { Id = tag.Id, Name = tag.Name };
            return null;
        }

        public async Task<bool> Save(TagDto entity)
        {
            return await tagRepository.Add(new Tag { Name = entity.Name });
        }
        public async Task<bool> Update(TagDto entity)
        {
            var tag = await tagRepository.GetById(entity.Id);
            if (tag == null)
                return false;
            tag.Name = entity.Name;
            return await tagRepository.Update(tag);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await tagRepository.Delete(id);
        }

    }
}
