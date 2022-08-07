using WBlog.Application.Core.Domain.Entity;
using WBlog.Application.Core.Domain;

namespace WBlog.Application.Core.Interfaces
{
    public interface IPostService
    {
        Task<FiltredData<Post>> GetPosts(ArticleRequestOptions options);
        Task<Post> GetPostById(Guid id);
        Task<IEnumerable<Tag>> GetPostTags(Guid id);
        Task<Post?> GetPostBySlug(string slug);
        Task<bool> Save(Post entity);
        Task<bool> Update(Post entity);
        Task<bool> Delete(Guid id);
        Task<bool> PublishPost(Guid id, bool publish);
    }
}
