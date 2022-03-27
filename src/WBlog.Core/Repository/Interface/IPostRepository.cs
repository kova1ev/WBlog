using WBlog.Domain.Entity;
using WBlog.Core.Dto;

namespace WBlog.Core.Repository.Interface
{
    public interface IPostRepository
    {
        Task<PostDetailsDto?> GetPostById(Guid id);
        Task<PostDetailsDto?> GetPostBySlug(string slug);
        Task<bool> RemovePost(Guid id);
        Task<bool> AddPost(Post post);
        Task<bool> UpdatePost(Post post);
        Task<bool> PublishPost(Guid id, bool publish);
        Task<IEnumerable<PostIndexDto>> SearchPost(string serchstr, SortState state, int offset, int limit);
        Task<IEnumerable<PostIndexDto>> GetPosts(string? tag, SortState state, int offset, int limit);
    }
}
