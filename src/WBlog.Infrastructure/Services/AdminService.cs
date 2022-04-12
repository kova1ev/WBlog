using Microsoft.EntityFrameworkCore;
using WBlog.Application.Domain.Entity;
using WBlog.Application.Core.Services;
using WBlog.Application.Core.Exceptions;
using WBlog.Application.Core.Dto;
using WBlog.Infrastructure.Data;
using WBlog.Infrastructure.Extantions;

namespace WBlog.Infrastructure.Services
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
            if (string.IsNullOrWhiteSpace(loginModel.Password))
                return false;
            string password = loginModel.Password.CreateHash(salt);
            Admin? admin = await GetAdmin(loginModel.Email!);
            if (admin == null)
                throw new ObjectNotFoundExeption($"Admin not found.");

            return password == admin.Password;
        }


        private async Task<Admin?> GetAdmin(string email)
        {
            return await dbcontext.Admin.FirstOrDefaultAsync(a => a.Email == email);
        }

    }
}
