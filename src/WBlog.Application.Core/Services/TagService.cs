using WBlog.Application.Core.Interfaces;
using WBlog.Application.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace WBlog.Application.Core.Services
{

    public class TagService : ITagService
    {
        private readonly ITagRepository tagRepository;
        public TagService(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public async Task<IEnumerable<Tag>> GetAllTags()
        {
            var result = await tagRepository.Tags.AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Tag>> GetTagsByPopularity()
        {

            var result = await tagRepository.Tags.AsNoTracking().Include(t => t.Posts)
                                    .OrderByDescending(t => t.Posts.Count)
                                    .ToListAsync();
            return result;
        }

        public async Task<Tag?> GetById(Guid id)
        {
            var tag = await tagRepository.GetById(id);
            if (tag != null)
                return tag;
            return null;
        }

        public async Task<Tag?> GetByName(string name)
        {
            var tag = await tagRepository.GetByName(name);
            if (tag != null)
                return tag;
            return null;
        }

        public async Task<bool> Save(Tag entity)
        {
            return await tagRepository.Add(new Tag { Name = entity.Name });
        }
        public async Task<bool> Update(Tag entity)
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
