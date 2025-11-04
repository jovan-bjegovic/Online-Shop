using FluentMigrator;
using OnlineShop.Core.Services;

namespace OnlineShop.Migrations.Migrations
{
    [Migration(7, "Create Users Manually")]
    public class CreateUsersManually : Migration
    {
        public override void Up()
        {
            var hasher = new PasswordHasher();

            string? admin1Password = Environment.GetEnvironmentVariable("ADMIN1_PASSWORD");
            string? admin2Password = Environment.GetEnvironmentVariable("ADMIN2_PASSWORD");
            string? userPassword = Environment.GetEnvironmentVariable("USER_PASSWORD");

            if (string.IsNullOrWhiteSpace(admin1Password) ||
                string.IsNullOrWhiteSpace(admin2Password) ||
                string.IsNullOrWhiteSpace(userPassword))
            {
                throw new InvalidOperationException("User passwords must be set in environment variables before running migration.");
            }

            Insert.IntoTable("Users").Row(new
            {
                Id = Guid.NewGuid(),
                Username = "admin1",
                PasswordHash = hasher.Hash(admin1Password),
                Role = "Admin",
                CreatedAt = DateTime.UtcNow
            });

            Insert.IntoTable("Users").Row(new
            {
                Id = Guid.NewGuid(),
                Username = "admin2",
                PasswordHash = hasher.Hash(admin2Password),
                Role = "Admin",
                CreatedAt = DateTime.UtcNow
            });

            Insert.IntoTable("Users").Row(new
            {
                Id = Guid.NewGuid(),
                Username = "user",
                PasswordHash = hasher.Hash(userPassword),
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
