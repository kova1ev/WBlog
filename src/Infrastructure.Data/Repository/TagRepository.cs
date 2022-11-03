using Microsoft.EntityFrameworkCore;
using WBlog.Infrastructure.Data;
using WBlog.Core.Interfaces;
using WBlog.Core.Domain.Entity;

namespace WBlog.Infrastructure.Data.Repository;

public class TagRepository : RepositoryBase<Tag>, ITagRepository
{
    public TagRepository(AppDbContext context) : base(context)
    {
    }

    public IQueryable<Tag> Tags => dbSet;

    public async Task<Tag?> GetByName(string name)
    {
        return await dbSet.FirstOrDefaultAsync(t => t.Name == name);
    }
}