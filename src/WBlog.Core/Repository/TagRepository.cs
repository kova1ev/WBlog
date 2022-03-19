using WBlog.Core.Repository.Interface;
using WBlog.Domain.Entity;
namespace WBlog.Core.Repository
{
    internal class TagRepository : ITagRepository
    {
        public Task<IEnumerable<Tag>> GetAllTags()
        {
            throw new NotImplementedException();
        }
    }
}
