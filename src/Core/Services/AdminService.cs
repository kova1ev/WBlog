using WBlog.Core.Domain.Entity;
using WBlog.Core.Exceptions;
using WBlog.Core.Extantions;
using WBlog.Core.Interfaces;
using WBlog.Core.Domain;

namespace WBlog.Core.Services;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _adminRepository;

    public AdminService(IAdminRepository adminRepository)
    {
        this._adminRepository = adminRepository;
    }

    public async Task<bool> Validation(Login login, string salt)
    {
        if (string.IsNullOrWhiteSpace(login.Password) || string.IsNullOrWhiteSpace(login.Email))
            return false;
        string password = login.Password.CreateHash(salt);
        Admin? admin = await _adminRepository.GetAdmin(login.Email);
        if (admin == null)
            throw new ObjectNotFoundExeption($"Admin not found.");

        return password == admin.Password;
    }
}