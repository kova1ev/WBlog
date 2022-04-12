﻿using Microsoft.EntityFrameworkCore;
using WBlog.Infrastructure.Data;
using WBlog.Application.Core.Services;
using WBlog.Application.Domain.Entity;

namespace WBlog.Infrastructure.Data.Repository
{
    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext context) : base(context) { }
        public IQueryable<Tag> Tags => dbSet.AsNoTracking();

        public async Task<IEnumerable<Tag>> GetAllTags()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<Tag?> GetByName(string name)
        {
            return await dbSet.FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower());
        }

    }
}
