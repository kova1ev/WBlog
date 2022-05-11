using WBlog.Application.Core.Dto;
using WBlog.Application.Domain.Entity;

namespace WBlog.Application.Core.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetAllTags();
        Task<IEnumerable<Tag>> GetTagsByPopularity();
        Task<Tag?> GetByName(string name);
        Task<Tag?> GetById(Guid id);
        Task<bool> Save(TagDto entity);
        Task<bool> Delete(Guid id);
        Task<bool> Update(TagDto entity);
    }
}
