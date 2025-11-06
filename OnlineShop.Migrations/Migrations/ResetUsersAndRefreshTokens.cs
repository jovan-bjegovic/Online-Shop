using FluentMigrator;
using OnlineShop.Core.Services;

namespace OnlineShop.Migrations.Migrations
{
    [Migration(15, "Reset Users Table and Seed 3 Users with RefreshTokens")]
    public class ResetUsersAndRefreshTokens : Migration
    {
        private string GenerateRefreshToken()
        {
            byte[] randomBytes = System.Security.Cryptography.RandomNumberGenerator.GetBytes(64);

            string refreshToken = Convert.ToBase64String(randomBytes);

            return refreshToken;
        }
        
        public override void Up()
        {
            if (Schema.Table("Users").Exists())
            {
                Delete.Table("Users");
            }

            Create.Table("Users")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Username").AsString().NotNullable().Unique()
                .WithColumn("PasswordHash").AsString().NotNullable()
                .WithColumn("Role").AsString().NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("RefreshToken").AsString().Nullable()
                .WithColumn("RefreshTokenExpiry").AsDateTime().Nullable();
            
            string? admin1Password = Environment.GetEnvironmentVariable("ADMIN1_PASSWORD");
            string? admin2Password = Environment.GetEnvironmentVariable("ADMIN2_PASSWORD");
            string? userPassword = Environment.GetEnvironmentVariable("USER_PASSWORD");
            
            if (string.IsNullOrWhiteSpace(admin1Password) ||
                string.IsNullOrWhiteSpace(admin2Password) ||
                string.IsNullOrWhiteSpace(userPassword))
            {
                throw new InvalidOperationException("User passwords must be set in environment variables before running migration.");
            }
            
            var hasher = new PasswordHasher();

            DateTime fixedCreatedAt = new DateTime(2025, 11, 5, 9, 0, 0, DateTimeKind.Utc);
            DateTime fixedRefreshExpiry = new DateTime(2025, 11, 12, 9, 0, 0, DateTimeKind.Utc);
            
            Insert.IntoTable("Users").Row(new
            {
                Id = Guid.NewGuid(),
                Username = "admin1",
                PasswordHash = hasher.Hash(admin1Password),
                Role = "Admin",
                CreatedAt = fixedCreatedAt,
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpiry = fixedRefreshExpiry
            });

            Insert.IntoTable("Users").Row(new
            {
                Id = Guid.NewGuid(),
                Username = "user",
                PasswordHash = hasher.Hash(userPassword),
                Role = "User",
                CreatedAt = fixedCreatedAt,
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpiry = fixedRefreshExpiry
            });

            Insert.IntoTable("Users").Row(new
            {
                Id = Guid.NewGuid(),
                Username = "Admin2",
                PasswordHash = hasher.Hash(admin2Password),
                Role = "Admin",
                CreatedAt = fixedCreatedAt,
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpiry = fixedRefreshExpiry
            });
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}
