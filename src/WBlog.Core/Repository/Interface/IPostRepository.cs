using WBlog.Domain.Entity;
using WBlog.Core.Dto;

namespace WBlog.Core.Repository.Interface
{
    public interface IPostRepository
    {
        Task<Post?> GetPostById(Guid id);
        Task<Post?> GetPostBySlug(string slug);
        Task<bool> RemovePost(Guid id);
        Task<bool> AddPost(Post post);
        Task<bool> UpdatePost(Post post);
        Task<bool> PublishPost(Guid id, bool publish);
        Task<IEnumerable<Post>> SearchPost(string serchstr, int offset, int limit, SortState state);
        Task<IEnumerable<Post>> GetPosts(int offset, int limit, SortState state);
        Task<IEnumerable<Post>> GetPostsByTag(string tag, int offset, int limit, SortState state);
    }
}
