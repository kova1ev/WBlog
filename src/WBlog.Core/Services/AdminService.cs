using Microsoft.EntityFrameworkCore;
using WBlog.Core.Dto.RequestModel;
using WBlog.Core.Exceptions;
using WBlog.Domain.Data;
using WBlog.Domain.Entity;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace WBlog.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext dbcontext;

        public AdminService(AppDbContext context)
        {
            this.dbcontext = context;
        }



        public async Task<bool> Validation(LoginModel loginModel, string salt)
        {
            string password = CreateHash(loginModel.Password!, salt);

            Admin? admin = await GetAdmin(loginModel.Email!);
            if (admin == null)
                throw new ObjectNotFoundExeption($"Admin not found.");

            if (password == admin.Password)
                return true;
            return false;
        }

        // todo сделать норманый метод создаия с солью
        public string CreateHash(string password, string salt)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            byte[] hash = KeyDerivation.Pbkdf2(
                            password: password,
                            salt: saltBytes,
                            prf: KeyDerivationPrf.HMACSHA256,
                            iterationCount: 100000,
                            numBytesRequested: 256 / 8);
            return Convert.ToBase64String(hash);
        }

        private async Task<Admin?> GetAdmin(string email)
        {
            return await dbcontext.Admin.FirstOrDefaultAsync();
        }

    }
}