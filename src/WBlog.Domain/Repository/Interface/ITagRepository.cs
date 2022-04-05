using WBlog.Domain.Entity;

namespace WBlog.Domain.Repository.Interface
{
    public interface ITagRepository : IRepository<Tag>
    {
        IQueryable<Tag> Tags { get; }
        Task<IEnumerable<Tag>> GetAllTags();
        Task<IEnumerable<Tag>> GetPopulasTags();
        Task<Tag?> GetByName(string name);
    }
}
