using WBlog.Core.Domain.Entity;

namespace WBlog.Core.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetById(Guid id);
    Task<bool> Add(T entity);
    Task<bool> Delete(T entity);
    Task<bool> Update(T entity);
}