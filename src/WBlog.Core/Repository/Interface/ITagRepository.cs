
using WBlog.Domain.Entity;

namespace WBlog.Core.Repository.Interface
{
    public interface ITagRepository
    {
        IQueryable<Tag> Tags { get; }
        Task<IEnumerable<Tag>> GetAllTags();
        Task<IEnumerable<Tag>> GetPopulasTags();
        Task<Tag?> GetById(Guid id);
        Task<Tag?> GetByName(string name);
        Task<bool> Remove(Guid id);
        Task<bool> Add(Tag tag);
        Task<bool> Update(Tag tag);
    }
}
