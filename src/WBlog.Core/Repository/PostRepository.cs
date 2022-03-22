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

        public Post GetPostById(Guid id)
        {
            return _dbContext.Posts.Include(p => p.Tags).FirstOrDefault(p => p.Id == id);
        }

        //////////////////////////////

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _dbContext.Posts.Include(p=>p.Tags).OrderBy(p => p.DateCreated).ToListAsync();
        }

        public async Task<Post?> GetPostByIdAsync(Guid id)
        {
            return await _dbContext.Posts.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Post>> GetPostsByTagAsync(string tag)
        {
            //return null!;
            if (tag != null) { }
            return await (from p in _dbContext.Posts
                          from t in p.Tags
                              //  where string.Equals(tag.ToLower(), t.Name.ToLower()) == true
                          where tag.ToLower().Equals(t.Name.ToLower())
                          orderby p.DateCreated descending
                          select p).ToListAsync();
            //return await _dbContext.Posts.Where(p => p.)
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
