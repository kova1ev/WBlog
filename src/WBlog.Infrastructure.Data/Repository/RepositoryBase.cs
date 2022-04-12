using Microsoft.EntityFrameworkCore;
using WBlog.Application.Core.Services;
using WBlog.Application.Domain.Entity;

namespace WBlog.Infrastructure.Data.Repository
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : BaseEntity
    {
        private AppDbContext context;
        internal DbSet<T> dbSet;
        public RepositoryBase(AppDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }
        public virtual async Task<T?> GetById(Guid id)
        {
            return await dbSet.FirstOrDefaultAsync(e=>e.Id==id);
        }

        public virtual async Task<bool> Add(T entity)
        {
            dbSet.Add(entity);
            return await context.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            var entity = await GetById(id);
            if (entity == null)
                return false;
            dbSet.Remove(entity);
            return await context.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> Update(T entity)
        {

            dbSet.Update(entity);
            return await context.SaveChangesAsync() > 0;
        }
    }
}