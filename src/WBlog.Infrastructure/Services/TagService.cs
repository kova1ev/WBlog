using WBlog.Application.Core.Interfaces;
using WBlog.Application.Domain.Entity;
using WBlog.Application.Core.Dto;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace WBlog.Infrastructure.Services
{
    public class TagService : ITagService
    {
        private readonly IMapper mapper;
        private readonly ITagRepository tagRepository;
        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            this.tagRepository = tagRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TagDto>> GetAllTags()
        {
            var result =await tagRepository.Tags.ToListAsync();
            return  mapper.Map<IEnumerable<TagDto>>(result);
        }

        public async Task<IEnumerable<PopularTagDto>> GetTagsByPopularity()
        {

            var result = await tagRepository.Tags.Include(t => t.Posts)
                                    .OrderByDescending(t => t.Posts.Count)
                                    .ToListAsync();
            return mapper.Map<IEnumerable<PopularTagDto>>(result);
        }

        public async Task<TagDto?> GetById(Guid id)
        {
            var tag = await tagRepository.GetById(id);
            if (tag != null)
                return mapper.Map<TagDto>(tag);
            return null;
        }

        public async Task<TagDto?> GetByName(string name)
        {
            var tag = await tagRepository.GetByName(name);
            if (tag != null)
                return mapper.Map<TagDto>(tag);
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
