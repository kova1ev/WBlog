
using WBlog.Domain.Entity;

namespace WBlog.Core.Repository.Interface
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllTags();
    }
}
