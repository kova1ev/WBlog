using WBlog.Application.Core.Dto;

namespace WBlog.Application.Core.Interfaces
{
    public interface IAdminService
    {
        Task<bool> Validation(LoginModel loginModel, string salt);
    }
}