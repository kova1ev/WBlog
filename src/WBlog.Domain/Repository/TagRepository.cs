using Microsoft.EntityFrameworkCore;
using WBlog.Domain.Data;
using WBlog.Domain.Repository.Interface;
using WBlog.Domain.Entity;

namespace WBlog.Domain.Repository
{
    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext context) :base(context) { }
        public IQueryable<Tag> Tags => dbSet.AsNoTracking();

        public async Task<IEnumerable<Tag>> GetAllTags()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<Tag?> GetByName(string name)
        {
            return await dbSet.FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower());
        }

        //todo подумать над реализацией
        public Task<IEnumerable<Tag>> GetPopulasTags()
        {
            throw new NotImplementedException();
        }

    }
}
