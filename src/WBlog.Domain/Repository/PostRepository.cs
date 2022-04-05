using Microsoft.EntityFrameworkCore;
using WBlog.Domain.Repository.Interface;
using WBlog.Domain.Data;
using WBlog.Domain.Entity;

namespace WBlog.Domain.Repository
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
