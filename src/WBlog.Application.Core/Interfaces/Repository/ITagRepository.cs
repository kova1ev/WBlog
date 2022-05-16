using WBlog.Application.Core.Domain.Entity;

namespace WBlog.Application.Core.Interfaces
{
    public interface ITagRepository : IRepository<Tag>
    {
        IQueryable<Tag> Tags { get; }
        Task<Tag?> GetByName(string name);
    }
}
