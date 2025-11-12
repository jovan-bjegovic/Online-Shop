using FluentMigrator;

namespace OnlineShop.Migrations.Migrations;

[Migration(19, "Add CreatedAt and UpdatedAt to all entities")]
public class AddAuditFields : Migration
{
    public override void Up()
    {
        Alter.Table("Categories")
            .AddColumn("CreatedAt").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
            .AddColumn("UpdatedAt").AsDateTime().Nullable();

        Alter.Table("Products")
            .AddColumn("CreatedAt").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
            .AddColumn("UpdatedAt").AsDateTime().Nullable();

        Alter.Table("ProductImages")
            .AddColumn("UpdatedAt").AsDateTime().Nullable();

        Alter.Table("Users")
            .AddColumn("UpdatedAt").AsDateTime().Nullable();
    }

    public override void Down()
    {
        Delete.Column("CreatedAt").FromTable("Categories");
        Delete.Column("UpdatedAt").FromTable("Categories");

        Delete.Column("CreatedAt").FromTable("Products");
        Delete.Column("UpdatedAt").FromTable("Products");

        Delete.Column("UpdatedAt").FromTable("ProductImages");

        Delete.Column("UpdatedAt").FromTable("Users");
    }
}
