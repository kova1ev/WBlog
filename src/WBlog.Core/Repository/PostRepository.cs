using Microsoft.EntityFrameworkCore;
using WBlog.Core.Repository.Interface;
using WBlog.Core.Data;
using WBlog.Core.Dto;
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

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _dbContext.Posts.OrderBy(p => p.DateCreated).ToListAsync();
        }


        public async Task<IEnumerable<Post>> GetPostsByTagAsync(string tag)
        {
            return await (from p in _dbContext.Posts
                          from t in p.Tags
                          where t.Name.ToLower() == tag.ToLower()
                          orderby p.DateCreated ascending//descending
                          select p).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByNameAsync(string name)
        {
            return await _dbContext.Posts.Where(p => p.Title.ToLower().Contains(name.ToLower()))
                                         .Select(p => p)
                                         .OrderBy(p => p.DateCreated)
                                         .ToListAsync();
        }
        //

        public async Task<PostDetailsDto?> GetPostById(Guid id)
        {
            var post = await _dbContext.Posts.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id);
            if (post != null)
            {
                new PostDetailsDto
                {
                    Id = post.Id,
                    DateUpdated = post.DateUpdated,
                    DateCreated = post.DateCreated,
                    Title = post.Title,
                    Slug = post.Slug,
                    Descriprion = post.Descriprion,
                    Contetnt = post.Contetnt,
                    ImagePath = string.Empty,
                    IsPublished = post.IsPublished,
                    Tags = post.Tags?.Select(t => t?.Name).ToArray()
                };
            }
            return null;
        }

        public async Task<PostDetailsDto?> GetPostBySlug(string slug)
        {
            var post = await _dbContext.Posts.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Slug == slug);
            if (post != null)
            {
                new PostDetailsDto
                {
                    Id = post.Id,
                    DateUpdated = post.DateUpdated,
                    DateCreated = post.DateCreated,
                    Title = post.Title,
                    Slug = post.Slug,
                    Descriprion = post.Descriprion,
                    Contetnt = post.Contetnt,
                    ImagePath = string.Empty,
                    IsPublished = post.IsPublished,
                    Tags = post.Tags?.Select(t => t?.Name).ToArray()
                };
            }
            return null;
        }

        // возврашать ентити а мапить в контроллере
        public async Task<IEnumerable<PostIndexDto>> GetPosts(string? tag, SortState state=0, int offset=0, int limit=0)
        {
            //var posts = _dbContext.Posts.SelectMany(p => p.Tags, (p, t) => new { Post = p, Tag = t })
            //    .Where(p => p.Tag.Name.ToLower() == tag.ToLower()).Select(a=>new PostIndexDto
            //    {
            //        Id=Post.Id

            //    }).ToListAsync();

            //return posts;
            //string s = state == SortState.DateAsc ? "ascending" : "descending";
            //return await (from p in _dbContext.Posts
            //              from t in p.Tags
            //              where t.Name.ToLower() == tag.ToLower()
            //              orderby p.DateCreated ascending
            //              select p).ToListAsync();
        }

        public async Task<IEnumerable<PostIndexDto>> SearchPost(string serchstr, SortState state, int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddPost(Post post)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemovePost(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdatePost(Post post)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> PublishPost(Guid id, bool publish)
        {
            throw new NotImplementedException();
        }
    }
}
