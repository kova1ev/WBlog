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

        //todo  объеденить с поиском по ключевому слову
        public async Task<IEnumerable<Post>> GetPosts(RequestOptions options)
        {
            var posts = from p in Posts.AsNoTracking()
                        from t in p.Tags
                        where (options.Tag == null || t.Name.ToLower() == options.Tag.ToLower())
                        select p;

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

        // искать в title & tag  ?   -- not work
        public async Task<IEnumerable<Post>> SearchPost(RequestOptions options)
        {
            var q = options.Query?.ToLower();
            var posts = (from p in Posts.AsNoTracking()
                             // from t in p.Tags
                         where q == null || p.Title.ToLower().Contains(q) //|| t.Name.ToLower().Contains(q)
                                                                          //  select p).Distinct();
                         select p);

            switch (options.State)
            {
                case SortState.DateAsc:
                    posts = posts.OrderBy(p => p.DateCreated);
                    break;
                case SortState.DateDesc:
                    posts = posts.OrderByDescending(p => p.DateCreated);
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
            var entity = await GetPostById(post.Id);
            if (entity == null)
                return false;
            post.DateUpdated = DateTime.Now;
            dbContext.Posts.Update(post);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> PublishPost(Guid id, bool publish)
        {
            var entity = await GetPostById(id);
            if (entity == null)
                return false;
            entity.IsPublished = publish;
            dbContext.Posts.Update(entity);
            return await dbContext.SaveChangesAsync() > 0;

        }

    }
}
