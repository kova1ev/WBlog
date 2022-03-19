using Microsoft.EntityFrameworkCore;
using WBlog.Domain.Entity;

namespace WBlog.Core.Data
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Tag> Tags => Set<Tag>();
    }
}
