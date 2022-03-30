using Microsoft.EntityFrameworkCore;
using WBlog.Core.Repository.Interface;
using WBlog.Core.Data;
using WBlog.Domain.Entity;

namespace WBlog.Core.Repository
{
    public class PostRepository : IPostRepository
    {
        readonly AppDbContext _dbContext;

        public PostRepository(AppDbContext context)
        {
            _dbContext = context;
        }

        //////////////////////////////

        // public async Task<IEnumerable<Post>> GetAllPostsAsync()
        // {
        //     return await _dbContext.Posts.AsNoTracking().OrderByDescending(p => p.DateCreated).ToListAsync();
        // }

        // public async Task<IEnumerable<Post>> GetPostsByNameAsync(string name)
        // {
        //     return await _dbContext.Posts.AsNoTracking().Where(p => p.Title.ToLower().Contains(name.ToLower()))
        //                                  .Select(p => p)
        //                                  .OrderBy(p => p.DateCreated)
        //                                  .ToListAsync();
        // }
        //

        public async Task<Post?> GetPostById(Guid id)
        {
            return await _dbContext.Posts.AsNoTracking().Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id);

        }

        public async Task<Post?> GetPostBySlug(string slug)
        {
            return await _dbContext.Posts.AsNoTracking().Include(p => p.Tags).FirstOrDefaultAsync(p => p.Slug == slug);
        }

        // объеденить поиск по ключевому слову + по тегам + сортировку + offset + limit
        public async Task<IEnumerable<Post>> GetPosts(RequestOptions options)
        {
            var posts = _dbContext.Posts.AsNoTracking().AsQueryable();
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

        public async Task<IEnumerable<Post>> GetPostsByTag(RequestOptions options)
        {
            //дублирует выдачу
            var posts = from p in _dbContext.Posts.AsNoTracking()
                        from t in p.Tags
                        where (options.Tag == null || t.Name.ToLower() == options.Tag.ToLower())
                        select p;

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

        // искать в title & tag  ?   -- not work
        public async Task<IEnumerable<Post>> SearchPost(RequestOptions options)
        {
            var q = options.Query?.ToLower();
            var posts = (from p in _dbContext.Posts.AsNoTracking()
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

        public async Task<bool> AddPost(Post post)
        {
            _dbContext.Posts.Add(post);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemovePost(Guid id)
        {
            var entity = await GetPostById(id);
            if (entity == null)
                return false;
            _dbContext.Posts.Remove(new Post { Id = id });
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePost(Post post)
        {
            var entity = await GetPostById(post.Id);
            if (entity == null)
                return false;
            post.DateUpdated = DateTime.Now;
            _dbContext.Posts.Update(post);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> PublishPost(Guid id, bool publish)
        {
            var entity = await GetPostById(id);
            if (entity == null)
                return false;
            entity.IsPublished = publish;
            _dbContext.Posts.Update(entity);
            return await _dbContext.SaveChangesAsync() > 0;

        }

        public async Task<IEnumerable<Tag>> GetPostsTags(Guid id)
        {
            return await _dbContext.Posts.AsNoTracking().Where(p => p.Id == id).SelectMany(p => p.Tags).ToListAsync();
        }
    }
}
