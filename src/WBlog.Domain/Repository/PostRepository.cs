using Microsoft.EntityFrameworkCore;
using WBlog.Domain.Repository.Interface;
using WBlog.Domain.Data;
using WBlog.Domain.Entity;

namespace WBlog.Domain.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext dbContext;
       
        public PostRepository(AppDbContext context)
        {
            dbContext = context;
        }

        public IQueryable<Post> Posts => dbContext.Posts;

        public async Task<Post?> GetPostById(Guid id)
        {
            return await Posts.AsNoTracking().Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Post?> GetPostBySlug(string slug)
        {
            return await Posts.AsNoTracking().Include(p => p.Tags).FirstOrDefaultAsync(p => p.Slug == slug);
        }       

        #region Тестовая реализация проверить/пробебажить
        public async Task<bool> Add(Post post)
        {
            dbContext.Posts.Add(post);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Remove(Guid id)
        {
            var entity = await GetPostById(id);
            if (entity == null)
                return false;
            dbContext.Posts.Remove(entity);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(Post post)
        {
            post.DateUpdated = DateTime.Now;
            dbContext.Entry(post).State = EntityState.Modified;
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> PublishPost(Guid id, bool publish)
        {
            var entity = await GetPostById(id);
            if (entity == null)
                return false;
            entity.DateUpdated = DateTime.Now;
            entity.IsPublished = publish;
            dbContext.Entry(entity).State = EntityState.Modified;
            return await dbContext.SaveChangesAsync() > 0;
        }
        #endregion

    }
}
