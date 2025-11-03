using FluentMigrator;

namespace OnlineShop.Migrations.Migrations
{
    [Migration(6, "Create Users Table")]
    public class CreateUsersTable : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Username").AsString(100).NotNullable().Unique()
                .WithColumn("PasswordHash").AsString(200).NotNullable()
                .WithColumn("Role").AsString(50).NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}