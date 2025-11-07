using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Models;

namespace OnlineShop.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Product> Products { get; set; }
    
    public DbSet<ProductImage> ProductImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
            
        modelBuilder.Entity<Category>(category =>
        {
            category.HasKey(c => c.Id);

            category.Property(c => c.Title)
                .IsRequired();

            category.Property(c => c.Code)
                .IsRequired();

            category.HasMany(c => c.Subcategories)
                .WithOne()
                .HasForeignKey(sc => sc.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            
            category.HasQueryFilter(c => !c.IsDeleted);
        });

        modelBuilder.Entity<Product>(product =>
        {
            product.HasKey(p => p.Id);

            product.Property(p => p.Title).IsRequired();
            product.Property(p => p.Sku).IsRequired();
            product.HasQueryFilter(p => !p.IsDeleted);

            product.HasOne<ProductImage>()     
                .WithMany()
                .HasForeignKey(p => p.ImageId)
                .OnDelete(DeleteBehavior.SetNull);
        });
        
        modelBuilder.Entity<ProductImage>(image =>
        {
            image.HasKey(i => i.Id);
        });
    }
}