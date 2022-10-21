using Microsoft.EntityFrameworkCore;
using WBlog.Core.Interfaces;
using WBlog.Core.Domain.Entity;

namespace WBlog.Infrastructure.Data.Repository;

public abstract class RepositoryBase<T> : IRepository<T> where T : BaseEntity
{
    private AppDbContext context;
    protected DbSet<T> dbSet;

    public RepositoryBase(AppDbContext context)
    {
        this.context = context;
        dbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetById(Guid id)
    {
        return await dbSet.FirstOrDefaultAsync(e => e.Id == id);
    }

    public virtual async Task<bool> Add(T entity)
    {
        dbSet.Add(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public virtual async Task<bool> Delete(T entity)
    {
        dbSet.Remove(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public virtual async Task<bool> Update(T entity)
    {
        dbSet.Update(entity);
        return await context.SaveChangesAsync() > 0;
    }
}