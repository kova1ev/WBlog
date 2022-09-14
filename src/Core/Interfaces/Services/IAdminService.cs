using WBlog.Core.Domain;

namespace WBlog.Core.Interfaces;

public interface IAdminService
{
    Task<bool> Validation(Login login, string salt);
}