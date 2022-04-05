using WBlog.Domain.Entity;

namespace WBlog.Domain.Repository.Interface
{
    public interface IPostRepository: IRepository<Post>
    {
        IQueryable<Post> Posts { get;}
        Task<Post?> GetPostBySlug(string slug);
    }
}
