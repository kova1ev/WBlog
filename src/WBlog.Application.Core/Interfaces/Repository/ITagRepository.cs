using WBlog.Application.Domain.Entity;

namespace WBlog.Application.Core.Services
{
    public interface ITagRepository : IRepository<Tag>
    {
        IQueryable<Tag> Tags { get; }
        Task<IEnumerable<Tag>> GetAllTags();
        Task<IEnumerable<Tag>> GetPopulasTags();
        Task<Tag?> GetByName(string name);
    }
}
