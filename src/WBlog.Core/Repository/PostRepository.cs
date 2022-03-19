using Microsoft.EntityFrameworkCore;
using WBlog.Core.Data;
using WBlog.Core.Repository.Interface;
using WBlog.Domain.Entity;
namespace WBlog.Core.Repository
{
    internal class PostRepository : IPostRepository
    {
        AppDbContext _dbContext;
        public PostRepository(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _dbContext.Posts.OrderBy(p => p.DateCreated).ToListAsync();
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            return await _dbContext.Posts.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<IEnumerable<Post>> GetPostsByTagAsync(string category)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetPostsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SavePostAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePostAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePostAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
