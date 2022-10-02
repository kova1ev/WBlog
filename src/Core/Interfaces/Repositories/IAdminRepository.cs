using WBlog.Core.Domain.Entity;

namespace WBlog.Core.Interfaces;

public interface IAdminRepository
{
    Task<User> GetAdmin(string email);
}