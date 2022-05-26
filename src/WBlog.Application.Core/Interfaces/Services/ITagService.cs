using WBlog.Application.Core.Domain.Entity;

namespace WBlog.Application.Core.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetAllTags();
        Task<IEnumerable<Tag>> GetTagsByPopularity();
        Task<Tag?> GetByName(string name);
        Task<Tag> GetById(Guid id);
        Task<bool> Save(Tag entity);
        Task<bool> Update(Tag entity);
        Task<bool> Delete(Guid id);
    }
}
