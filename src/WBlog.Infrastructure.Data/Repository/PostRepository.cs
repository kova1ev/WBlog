using Microsoft.EntityFrameworkCore;
using WBlog.Application.Core.Interfaces;
using WBlog.Application.Core.Domain.Entity;

namespace WBlog.Infrastructure.Data.Repository
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context) { }

        public IQueryable<Post> Posts => dbSet;

        public override async Task<Post?> GetById(Guid id)
        {
            return await dbSet.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id);
        }

    }
}
