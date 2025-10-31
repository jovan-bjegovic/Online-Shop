using FluentMigrator;

namespace OnlineShop.Migrations.Migrations;

[Migration(2, "Create categories table without constraints")]
public class CreateCategoriesTableWithoutConstraints : Migration
{
    public override void Up()
    {
        Alter.Column("Title").OnTable("Categories").AsString().NotNullable();

        Alter.Table("Categories").AddColumn("Code").AsString().NotNullable();

        Alter.Table("Categories").AddColumn("Description").AsString().Nullable();
    }

    public override void Down()
    {
        if (Schema.Table("Categories").Exists())
        {
            Delete.Table("Categories");
        }
    }
}