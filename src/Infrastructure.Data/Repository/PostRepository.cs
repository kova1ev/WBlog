using Microsoft.EntityFrameworkCore;
using WBlog.Core.Interfaces;
using WBlog.Core.Domain.Entity;

namespace WBlog.Infrastructure.Data.Repository;

public class PostRepository : RepositoryBase<Post>, IPostRepository
{
    public PostRepository(AppDbContext context) : base(context)
    {
    }

    public IQueryable<Post> Posts => dbSet;

    public override async Task<Post?> GetByIdAsync(Guid id)
    {
        return await dbSet.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id);
    }
}