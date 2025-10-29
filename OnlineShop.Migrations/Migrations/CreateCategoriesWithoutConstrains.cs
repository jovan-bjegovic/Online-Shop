using FluentMigrator;

namespace OnlineShop.Migrations.Migrations;

[Migration(2, "Create categories table without constraints")]
public class CreateCategoriesTableWithoutConstraints : Migration
{
    public override void Up()
    {
        if (Schema.Table("Categories").Exists())
        {
            Alter.Column("Title").OnTable("Categories").AsString().NotNullable();

            if (!Schema.Table("Categories").Column("Code").Exists())
            {
                Alter.Table("Categories").AddColumn("Code").AsString().NotNullable();
            }

            if (!Schema.Table("Categories").Column("Description").Exists())
            {
                Alter.Table("Categories").AddColumn("Description").AsString().Nullable();
            }
        }
        else
        {
            Create.Table("Categories")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Title").AsString(100).NotNullable()
                .WithColumn("Code").AsString(50).NotNullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("ParentCategoryId").AsGuid().Nullable();

            Create.ForeignKey("FK_Categories_Parent")
                .FromTable("Categories").ForeignColumn("ParentCategoryId")
                .ToTable("Categories").PrimaryColumn("Id")
                .OnDelete(System.Data.Rule.None);
        }
    }

    public override void Down()
    {
        if (Schema.Table("Categories").Exists())
        {
            Delete.Table("Categories");
        }
    }
}