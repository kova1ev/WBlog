using WBlog.Application.Domain.Entity;

namespace WBlog.Application.Core.Services
{
    public interface IPostRepository: IRepository<Post>
    {
        IQueryable<Post> Posts { get;}
        Task<Post?> GetPostBySlug(string slug);
    }
}
