using Microsoft.EntityFrameworkCore;
using WBlog.Domain.Entity;

namespace WBlog.Domain.Repository.Interface
{
    public interface IRepository<T> where T :BaseEntity
    {
        Task<T?> GetById(Guid id);
        Task<bool> Add(T entity);
        Task<bool> Delete(Guid id);
        Task<bool> Update(T entity);

    }
}