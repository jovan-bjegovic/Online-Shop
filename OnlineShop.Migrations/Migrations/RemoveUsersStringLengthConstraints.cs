using FluentMigrator;

namespace OnlineShop.Migrations.Migrations
{
    [Migration(9, "Remove length constraints from Users table")]
    public class RemoveUsersStringLengthConstraints : Migration
    {
        public override void Up()
        {
            Alter.Column("Username").OnTable("Users").AsString(int.MaxValue).NotNullable();
            Alter.Column("PasswordHash").OnTable("Users").AsString(int.MaxValue).NotNullable();
            Alter.Column("Role").OnTable("Users").AsString(int.MaxValue).NotNullable();
        }

        public override void Down()
        {
            Alter.Column("Username").OnTable("Users").AsString(100).NotNullable();
            Alter.Column("PasswordHash").OnTable("Users").AsString(200).NotNullable();
            Alter.Column("Role").OnTable("Users").AsString(50).NotNullable();
        }
    }
}