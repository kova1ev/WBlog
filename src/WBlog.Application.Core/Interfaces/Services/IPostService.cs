using WBlog.Application.Core.Dto;
using WBlog.Application.Core;
using WBlog.Application.Core.Entity;

namespace WBlog.Application.Core.Interfaces
{
    public interface IPostService
    {
        Task<FiltredPosts> GetPosts(RequestOptions options);
        Task<Post?> GetPostById(Guid id);
        Task<IEnumerable<Tag>> GetPostTags(Guid id);
        Task<Post?> GetPostBySlug(string slug);
        Task<bool> Save(PostEditDto postEdit);
        Task<bool> Update(PostEditDto post);
        Task<bool> Delete(Guid id);
        Task<bool> PublishPost(Guid id, bool publish);
    }
}
