using FluentMigrator;

namespace OnlineShop.Migrations.Migrations;

[Migration(1)]
public class CreateCategoriesTable : Migration
{
    public override void Up()
    {
        Create.Table("Categories")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Title").AsString(50).NotNullable()
            .WithColumn("Code").AsString(20).NotNullable()
            .WithColumn("Description").AsString(500).Nullable()
            .WithColumn("ParentCategoryId").AsGuid().Nullable();
            
        Create.ForeignKey("FK_Categories_Parent")
            .FromTable("Categories").ForeignColumn("ParentCategoryId")
            .ToTable("Categories").PrimaryColumn("Id")
            .OnDelete(System.Data.Rule.None);
    }

    public override void Down()
    {
        Delete.Table("Categories");
    }
}