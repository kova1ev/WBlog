using Microsoft.EntityFrameworkCore;
using WBlog.Core.Repository.Interface;
using WBlog.Core.Data;
using WBlog.Domain.Entity;

namespace WBlog.Core.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext dbContext;

        public IQueryable<Post> Posts => dbContext.Posts;

        public PostRepository(AppDbContext context)
        {
            dbContext = context;
        }

        //////////////////////////////

        public async Task<Post?> GetPostById(Guid id)
        {
            return await Posts.AsNoTracking().Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id);

        }

        public async Task<Post?> GetPostBySlug(string slug)
        {
            return await Posts.AsNoTracking().Include(p => p.Tags).FirstOrDefaultAsync(p => p.Slug == slug);
        }

        //todo  вынести tolower() в RequestOptions
        //todo если не выбран тег, то поиск сделать и в тегах и в заголовках
        public async Task<IEnumerable<Post>> GetPosts(RequestOptions options)
        {
            IQueryable<Post> posts = Posts.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(options.Tag))
            {
                posts = from p in posts
                        from t in p.Tags
                        where t.Name.ToLower() == options.Tag.ToLower()
                        select p;
            }
            if (!string.IsNullOrWhiteSpace(options.Query))
            {
                posts = posts.Where(p => p.Title.ToLower().Contains(options.Query.ToLower()));
            }


            switch (options.State)
            {
                case SortState.DateAsc:
                    posts = posts.OrderBy(p => p.DateCreated);
                    break;
                default:
                    posts = posts.OrderByDescending(p => p.DateCreated);
                    break;
            }
            return await posts.Skip(options.OffSet).Take(options.Limit).ToArrayAsync();
        }

        public async Task<IEnumerable<Tag>> GetPostsTags(Guid id)
        {
            return await dbContext.Posts.AsNoTracking().Where(p => p.Id == id).SelectMany(p => p.Tags).ToListAsync();
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
