using WBlog.Core.Repository.Interface;
using WBlog.Domain.Entity;
namespace WBlog.Core.Repository
{
    public class TagRepository : ITagRepository
    {
        public Task<IEnumerable<Tag>> GetAllTags()
        {
            throw new NotImplementedException();
        }
    }
}
