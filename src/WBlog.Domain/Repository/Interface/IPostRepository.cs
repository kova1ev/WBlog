using WBlog.Domain.Entity;

namespace WBlog.Domain.Repository.Interface
{
    public interface IPostRepository
    {
        IQueryable<Post> Posts { get;}
        Task<Post?> GetPostById(Guid id);
        Task<Post?> GetPostBySlug(string slug);
        Task<bool> Remove(Guid id);
        Task<bool> Add(Post post);
        Task<bool> Update(Post post);
    }
}
