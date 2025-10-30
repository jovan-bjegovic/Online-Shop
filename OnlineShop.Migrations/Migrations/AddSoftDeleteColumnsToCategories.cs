using FluentMigrator;

namespace OnlineShop.Migrations.Migrations;

[Migration(3, "Add soft-delete columns to Categories table")]
public class AddSoftDeleteColumnsToCategories : Migration
{
    public override void Up()
    {
        if (!Schema.Table("Categories").Column("IsDeleted").Exists())
        {
            Alter.Table("Categories")
                .AddColumn("IsDeleted").AsBoolean().WithDefaultValue(false).NotNullable();
        }

        if (!Schema.Table("Categories").Column("DeletedAt").Exists())
        {
            Alter.Table("Categories")
                .AddColumn("DeletedAt").AsDateTime().Nullable();
        }
    }

    public override void Down()
    {
        if (!Schema.Table("Categories").Exists())
        {
            return;
        }
        if (Schema.Table("Categories").Column("IsDeleted").Exists())
        {
            Delete.Column("IsDeleted").FromTable("Categories");
        }

        if (Schema.Table("Categories").Column("DeletedAt").Exists())
        {
            Delete.Column("DeletedAt").FromTable("Categories");
        }
    }
}
