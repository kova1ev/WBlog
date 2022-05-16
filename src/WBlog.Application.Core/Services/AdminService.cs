using WBlog.Application.Core.Domain.Entity;
using WBlog.Application.Core.Exceptions;
using WBlog.Application.Core.Extantions;
using WBlog.Application.Core.Interfaces;
using WBlog.Application.Core.Domain;

namespace WBlog.Application.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }

        public async Task<bool> Validation(Login login, string salt)
        {
            if (string.IsNullOrWhiteSpace(login.Password) || string.IsNullOrWhiteSpace(login.Email))
                return false;
            string password = login.Password.CreateHash(salt);
            Admin? admin = await adminRepository.GetAdmin(login.Email);
            if (admin == null)
                throw new ObjectNotFoundExeption($"Admin not found.");

            return password == admin.Password;
        }
    }
}
