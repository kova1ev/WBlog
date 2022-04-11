using WBlog.Application.Core.Dto;

namespace WBlog.Application.Core.Services
{
    public interface IAdminService
    {
        Task<bool> Validation(LoginModel loginModel, string salt);
    }
}