using FluentMigrator;

namespace OnlineShop.Migrations.Migrations;

[Migration(3, "Add soft-delete columns to Categories table")]
public class AddSoftDeleteColumnsToCategories : Migration
{
    public override void Up()
    {
        Alter.Table("Categories")
            .AddColumn("IsDeleted").AsBoolean().WithDefaultValue(false).NotNullable();
        Alter.Table("Categories")
                .AddColumn("DeletedAt").AsDateTime().Nullable();
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
