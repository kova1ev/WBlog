using WBlog.Core.Domain.Entity;

namespace WBlog.Core.Interfaces;

public interface IAdminRepository
{
    Task<Admin> GetAdmin(string email);
}