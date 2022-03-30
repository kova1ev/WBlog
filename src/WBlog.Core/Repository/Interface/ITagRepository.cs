
using WBlog.Domain.Entity;

namespace WBlog.Core.Repository.Interface
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllTags();
        Task<IEnumerable<Tag>> GetPopulasTags();
        Task<bool> Remove(Guid id);
        Task<bool> Add(Tag tag);
        Task<bool> Update(Tag tag);
    }
}
