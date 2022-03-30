using WBlog.Domain.Entity;
using WBlog.Core.Dto;

namespace WBlog.Core.Repository.Interface
{
    public interface IPostRepository
    {
        IQueryable<Post> Posts { get;}
        Task<Post?> GetPostById(Guid id);
        Task<Post?> GetPostBySlug(string slug);
        Task<bool> Remove(Guid id);
        Task<bool> Add(Post post);
        Task<bool> Update(Post post);
        Task<bool> PublishPost(Guid id, bool publish);
        Task<IEnumerable<Post>> GetPosts(RequestOptions options);
        Task<IEnumerable<Tag>> GetPostsTags(Guid id);
    }
}
