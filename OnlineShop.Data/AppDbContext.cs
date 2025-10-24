using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Models;

namespace OnlineShop.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
            
        modelBuilder.Entity<Category>(category =>
        {
            category.HasKey(c => c.Id);

            category.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(50);

            category.Property(c => c.Code)
                .IsRequired()
                .HasMaxLength(20);

            category.Property(c => c.Description)
                .HasMaxLength(500);

            category.HasMany(c => c.Subcategories)
                .WithOne()
                .HasForeignKey(sc => sc.ParentCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}