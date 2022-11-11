using Microsoft.EntityFrameworkCore;
using WBlog.Core.Domain.Entity;

namespace WBlog.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Tag> Tags => Set<Tag>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Tag>().HasIndex(t => t.NormalizeName).IsUnique();
        builder.Entity<Post>().HasIndex(p => p.NormalizeSlug).IsUnique();

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

    }
}
