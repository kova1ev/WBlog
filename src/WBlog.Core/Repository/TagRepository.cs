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

        public Task<IEnumerable<Tag>> GetPopulasTags()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Tag tag)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Add(Tag tag)
        {
            throw new NotImplementedException();
        }
    }
}
