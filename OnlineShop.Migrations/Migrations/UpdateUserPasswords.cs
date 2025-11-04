using FluentMigrator;
using OnlineShop.Core.Services;
using System;

namespace OnlineShop.Migrations.Migrations
{
    [Migration(10, "Update Users Passwords from Environment")]
    public class UpdateUserPasswords : Migration
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

            Update.Table("Users")
                .Set(new { PasswordHash = hasher.Hash(admin1Password) })
                .Where(new { Username = "admin1" });

            Update.Table("Users")
                .Set(new { PasswordHash = hasher.Hash(admin2Password) })
                .Where(new { Username = "admin2" });

            Update.Table("Users")
                .Set(new { PasswordHash = hasher.Hash(userPassword) })
                .Where(new { Username = "user" });
        }

        public override void Down()
        {
        }
    }
}