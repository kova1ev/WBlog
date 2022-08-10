using WBlog.Application.Core.Domain;
using WBlog.Application.Core.Domain.Entity;

namespace WBlog.Application.Core.Interfaces
{
    public interface ITagService
    {
        Task<FiltredData<Tag>> GetTags(TagRequestOptions options);
        Task<IEnumerable<Tag>> GetTagsByPopularity(int count);
        Task<Tag?> GetByName(string name);
        Task<Tag> GetById(Guid id);
        Task<bool> Save(Tag entity);
        Task<bool> Update(Tag entity);
        Task<bool> Delete(Guid id);
    }
}
