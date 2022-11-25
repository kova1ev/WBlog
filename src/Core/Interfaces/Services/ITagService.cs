using System.Diagnostics.CodeAnalysis;
using WBlog.Core.Domain;
using WBlog.Core.Domain.Entity;

namespace WBlog.Core.Interfaces;

public interface ITagService
{
    Task<FilteredData<Tag>> GetTagsAsync(TagRequestOptions options);
    Task<IEnumerable<Tag>> GetTagsByPopularityAsync(int count);
    Task<Tag?> GetByNameAsync(string name);
    Task<Tag> GetByIdAsync(Guid id);
    Task<bool> SaveAsync(Tag entity);
    Task<bool> UpdateAsync(Tag entity);
    Task<bool> DeleteAsync(Guid id);
    [return: NotNullIfNotNull("name")]
    string? NormalizeName(string? name);
}