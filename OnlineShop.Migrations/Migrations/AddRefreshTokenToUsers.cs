using FluentMigrator;

namespace OnlineShop.Migrations.Migrations;

[Migration(8, "AddRefreshTokenToUsers")]
public class AddRefreshTokenToUsers : Migration
{
    public override void Up()
    {
        Alter.Table("Users")
            .AddColumn("RefreshToken").AsString(256).Nullable()
            .AddColumn("RefreshTokenExpiry").AsDateTime().Nullable();
    }

    public override void Down()
    {
        Delete.Column("RefreshToken").FromTable("Users");
        Delete.Column("RefreshTokenExpiry").FromTable("Users");
    }
}