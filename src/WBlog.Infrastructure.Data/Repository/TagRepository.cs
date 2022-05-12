using Microsoft.EntityFrameworkCore;
using WBlog.Infrastructure.Data;
using WBlog.Application.Core.Interfaces;
using WBlog.Application.Core.Entity;

namespace WBlog.Infrastructure.Data.Repository
{
    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext context) : base(context) { }
        public IQueryable<Tag> Tags => dbSet;

        public async Task<Tag?> GetByName(string name)
        {
            return await dbSet.FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower());
        }

    }
}
