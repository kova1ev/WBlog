using WBlog.Application.Domain.Entity;
using WBlog.Application.Core.Exceptions;
using WBlog.Application.Core.Dto;
using WBlog.Infrastructure.Extantions;
using WBlog.Application.Core.Interfaces;

namespace WBlog.Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }

        public async Task<bool> Validation(LoginModel loginModel, string salt)
        {
            if (string.IsNullOrWhiteSpace(loginModel.Password) || string.IsNullOrWhiteSpace(loginModel.Email))
                return false;
            string password = loginModel.Password.CreateHash(salt);
            Admin? admin = await adminRepository.GetAdmin(loginModel.Email);
            if (admin == null)
                throw new ObjectNotFoundExeption($"Admin not found.");

            return password == admin.Password;
        }
    }
}
