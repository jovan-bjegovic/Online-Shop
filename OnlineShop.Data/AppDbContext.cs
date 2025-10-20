using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Models;

namespace OnlineShop.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
}