using Microsoft.EntityFrameworkCore;

namespace WBlog.Domain.Repository
{
    public interface IRepository<T> where T : DbContext
    {

    }
}