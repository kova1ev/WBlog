using WBlog.Core.Domain.Entity;

namespace WBlog.Core.Interfaces;

public interface IPostRepository : IRepository<Post>
{
    IQueryable<Post> Posts { get; }
}