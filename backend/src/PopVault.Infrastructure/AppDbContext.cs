using Microsoft.EntityFrameworkCore;
using PopVault.Domain.Entities;

namespace PopVault.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Hardcoded for Console App simplicity. In Production, use Configuration.
        optionsBuilder.UseSqlite("Data Source=popvault.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Fluent API configurations (optional, but good practice)
        base.OnModelCreating(modelBuilder);
    }
}
