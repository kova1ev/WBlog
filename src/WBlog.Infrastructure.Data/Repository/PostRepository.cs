using Microsoft.EntityFrameworkCore;
using WBlog.Application.Core.Services;
using WBlog.Application.Domain.Entity;

namespace WBlog.Infrastructure.Data.Repository
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context) { }

        public IQueryable<Post> Posts => dbSet.AsNoTracking();

        public override async Task<Post?> GetById(Guid id)
        {
            return await dbSet.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Post?> GetPostBySlug(string slug)
        {
            return await dbSet.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Slug == slug);
        }

    }
}
