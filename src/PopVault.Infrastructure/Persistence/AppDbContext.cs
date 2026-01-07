using Microsoft.EntityFrameworkCore;
using PopVault.Domain.Entities;

namespace PopVault.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Album> Albuns { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Titulo).IsRequired();
            entity.Property(e => e.Artista).IsRequired();
            // Add other configurations as needed
        });
    }
}
