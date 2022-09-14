using Microsoft.EntityFrameworkCore;
using WBlog.Core.Interfaces;
using WBlog.Core.Domain.Entity;

namespace WBlog.Infrastructure.Data.Repository;

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