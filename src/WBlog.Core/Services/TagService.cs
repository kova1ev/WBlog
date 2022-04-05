using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBlog.Core.Dto;
using WBlog.Domain.Entity;
using WBlog.Domain.Repository.Interface;

namespace WBlog.Core.Services
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
        public Task<IEnumerable<TagDto>> GetPopulasTags()
        {
            throw new NotImplementedException();
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
            return await tagRepository.Add(new Tag { Name=entity.Name});
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
