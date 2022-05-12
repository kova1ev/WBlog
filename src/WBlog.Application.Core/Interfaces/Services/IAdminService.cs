using WBlog.Application.Core.Models;

namespace WBlog.Application.Core.Interfaces
{
    public interface IAdminService
    {
        Task<bool> Validation(LoginModel loginModel, string salt);
    }
}