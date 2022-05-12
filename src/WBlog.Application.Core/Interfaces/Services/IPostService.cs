using WBlog.Application.Core;
using WBlog.Application.Core.Entity;
using WBlog.Application.Core.Models;

namespace WBlog.Application.Core.Interfaces
{
    public interface IPostService
    {
        Task<FiltredPosts> GetPosts(RequestOptions options);
        Task<Post?> GetPostById(Guid id);
        Task<IEnumerable<Tag>> GetPostTags(Guid id);
        Task<Post?> GetPostBySlug(string slug);
        //save post or postmodel
        Task<bool> Save(PostModel postEdit);
        Task<bool> Update(PostModel post);
        Task<bool> Delete(Guid id);
        Task<bool> PublishPost(Guid id, bool publish);
    }
}
