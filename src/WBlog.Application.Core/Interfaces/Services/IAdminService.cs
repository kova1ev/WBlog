using WBlog.Application.Core.Domain;

namespace WBlog.Application.Core.Interfaces
{
    public interface IAdminService
    {
        Task<bool> Validation(Login login, string salt);
    }
}