using WBlog.Application.Core.Entity;

namespace WBlog.Application.Core.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        IQueryable<Post> Posts { get; }
    }
}
