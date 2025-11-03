using FluentMigrator;

namespace OnlineShop.Migrations.Migrations
{
    [Migration(7, "Create Users Manually")]
    public class CreateUsersManually : Migration
    {
        public override void Up()
        {
            Insert.IntoTable("Users").Row(new
            {
                Id = Guid.NewGuid(),
                Username = "admin1",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Role = "Admin",
                CreatedAt = DateTime.UtcNow
            });

            Insert.IntoTable("Users").Row(new
            {
                Id = Guid.NewGuid(),
                Username = "admin2",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin456"),
                Role = "Admin",
                CreatedAt = DateTime.UtcNow
            });

            Insert.IntoTable("Users").Row(new
            {
                Id = Guid.NewGuid(),
                Username = "user",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"),
                Role = "User",
                CreatedAt = DateTime.UtcNow
            });
        }

        public override void Down()
        {
            Delete.FromTable("Users").Row(new { Username = "admin1" });
            Delete.FromTable("Users").Row(new { Username = "admin2" });
            Delete.FromTable("Users").Row(new { Username = "user" });
        }
    }
}