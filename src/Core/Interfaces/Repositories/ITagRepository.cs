using WBlog.Core.Domain.Entity;

namespace WBlog.Core.Interfaces;

public interface ITagRepository : IRepository<Tag>
{
    IQueryable<Tag> Tags { get; }
    Task<Tag?> GetByNameAsync(string normalizeName);
}