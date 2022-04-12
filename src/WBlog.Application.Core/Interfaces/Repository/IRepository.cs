using WBlog.Application.Domain.Entity;

namespace WBlog.Application.Core.Services
{
    public interface IRepository<T> where T :BaseEntity
    {
        Task<T?> GetById(Guid id);
        Task<bool> Add(T entity);
        Task<bool> Delete(Guid id);
        Task<bool> Update(T entity);

    }
}