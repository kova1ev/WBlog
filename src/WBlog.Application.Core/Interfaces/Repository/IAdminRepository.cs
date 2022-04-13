
using WBlog.Application.Domain.Entity;

namespace WBlog.Application.Core.Interfaces
{
    public interface IAdminRepository
    {
        Task<Admin?> GetAdmin(string email);
    }
}
