using FluentMigrator;

namespace OnlineShop.Migrations.Migrations;

[Migration(3, "Add soft-delete columns to Categories table")]
public class AddSoftDeleteColumnsToCategories : Migration
{
    public override void Up()
    {
        if (Schema.Table("Categories").Exists())
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
        else
        {
            Create.Table("Categories")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("Code").AsString().NotNullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("ParentCategoryId").AsGuid().Nullable()
                .WithColumn("IsDeleted").AsBoolean().WithDefaultValue(false).NotNullable()
                .WithColumn("DeletedAt").AsDateTime().Nullable();

            Create.ForeignKey("FK_Categories_Parent")
                .FromTable("Categories").ForeignColumn("ParentCategoryId")
                .ToTable("Categories").PrimaryColumn("Id")
                .OnDelete(System.Data.Rule.None);
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
