using WBlog.Core.Domain.Entity;
using WBlog.Core.Domain;
using System.Diagnostics.CodeAnalysis;

namespace WBlog.Core.Interfaces;

public interface IPostService
{
    Task<FiltredData<Post>> GetPostsAsync(ArticleRequestOptions options);
    Task<Post> GetPostByIdAsync(Guid id);
    Task<IEnumerable<Tag>> GetPostTagsAsync(Guid id);
    Task<Post?> GetPostBySlugAsync(string slug);
    Task<bool> SaveAsync(Post entity);
    Task<bool> UpdateAsync(Post entity);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> PublishPostAsync(Guid id, bool publish);
    [return: NotNullIfNotNull("slug")]
    string? NormalizeSlug(string? slug);
}