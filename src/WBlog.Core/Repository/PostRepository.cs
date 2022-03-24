﻿using Microsoft.EntityFrameworkCore;
using WBlog.Core.Data;
using WBlog.Core.Repository.Interface;
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

        public async Task<Post?> GetPostByIdAsync(Guid id)
        {
            return await _dbContext.Posts.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Post>> GetPostsByTagAsync(string tag)
        {
            return await (from p in _dbContext.Posts
                          from t in p.Tags
                          where t.Name.ToLower() == tag.ToLower()
                          orderby p.DateCreated descending
                          select p).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByNameAsync(string name)
        {
            return await _dbContext.Posts.Where(p => p.Title.ToLower().Contains(name.ToLower()))
                                         .Select(p => p)
                                         .OrderBy(p => p.DateCreated)
                                         .ToListAsync();
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
