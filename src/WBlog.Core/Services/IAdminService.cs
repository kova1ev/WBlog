using WBlog.Core.Dto.RequestModel;

namespace WBlog.Core.Services
{
    public interface IAdminService
    {
        Task<bool> Validation(LoginModel loginModel, string salt);
    }
}