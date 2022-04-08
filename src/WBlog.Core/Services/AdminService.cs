using Microsoft.EntityFrameworkCore;
using WBlog.Domain.Data;
using WBlog.Domain.Entity;

namespace WBlog.Core.Services
{
    public class AdminService :IAdminService
    {
        private readonly AppDbContext context;

        public AdminService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<Admin?> GetAdmin()
        {
            return  await context.Admin.FirstOrDefaultAsync();
        }

       // todo сделать норманый метод создаия с солью
        public string CreateHash(string password) // salt = соль
        {
            string salt = "qwerty"; //todo get from config
            string result = (password+salt).GetHashCode().ToString(); // make hash
            return result;
        }
    }
}