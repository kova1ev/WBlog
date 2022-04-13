using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBlog.Application.Core.Interfaces;
using WBlog.Application.Domain.Entity;

namespace WBlog.Infrastructure.Data.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext context;

        public AdminRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<Admin?> GetAdmin(string email)
        {
            return await context.Admin.FirstOrDefaultAsync(a => a.Email == email);
        }
    }
}
