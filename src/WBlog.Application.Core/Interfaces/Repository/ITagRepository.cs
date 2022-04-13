﻿using WBlog.Application.Domain.Entity;

namespace WBlog.Application.Core.Interfaces
{
    public interface ITagRepository : IRepository<Tag>
    {
        IQueryable<Tag> Tags { get; }
        Task<IEnumerable<Tag>> GetAllTags();
        Task<Tag?> GetByName(string name);
    }
}
