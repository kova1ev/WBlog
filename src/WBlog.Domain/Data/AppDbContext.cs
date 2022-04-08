using Microsoft.EntityFrameworkCore;
using WBlog.Domain.Entity;
using WBlog.Domain.Repository;

namespace WBlog.Domain.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Admin> Admin => Set<Admin>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Tag> Tags => Set<Tag>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Admin>().HasData(new Admin
            {
                Id = new Guid("447492f2-23cf-4f3a-9f65-4b4b96a52b0d"),
                Email = "admin@gmail.com",
                // получать 
                Password = new AdminRepository(this).CreateHash("12345") //todo hash

            });
        }
    }

}