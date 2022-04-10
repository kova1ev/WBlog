using Microsoft.EntityFrameworkCore;
using WBlog.Core.Dto.RequestModel;
using WBlog.Core.Exceptions;
using WBlog.Domain.Data;
using WBlog.Domain.Entity;

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
            var password = "-1619820305";// CreateHash(loginModel.Password);

            Admin? admin = await GetAdmin(loginModel.Email);
            if (admin == null)
                throw new ObjectNotFoundExeption($"Admin not found.");

            if (password == admin.Password)
                return true;
            return false;
        }

        // todo сделать норманый метод создаия с солью
        public string CreateHash(string password) // salt = соль
        {
            string salt = "qwerty"; //todo get from config
            string result = (password + salt).GetHashCode().ToString(); // make hash
            return result;
        }

        private async Task<Admin?> GetAdmin(string email)
        {
            return await dbcontext.Admin.FirstOrDefaultAsync();
        }

    }
}