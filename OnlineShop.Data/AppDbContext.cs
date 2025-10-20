using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Models;

namespace OnlineShop.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
            
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(c => c.Code)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(c => c.Description)
                .HasMaxLength(500);

            entity.HasMany(c => c.Subcategories)
                .WithOne()
                .HasForeignKey(sc => sc.ParentCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}