using FluentMigrator;

namespace OnlineShop.Migrations.Migrations;

[Migration(20, "Rename ProductImages table to Images")]
public class RenameProductImagesTable : Migration
{
    public override void Up()
    {
        Rename.Table("ProductImages").To("Images");
    }

    public override void Down()
    {
        Rename.Table("Images").To("ProductImages");
    }
}
