using WBlog.Application.Core.Domain.Entity;

namespace WBlog.Application.Core.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        IQueryable<Post> Posts { get; }
    }
}
