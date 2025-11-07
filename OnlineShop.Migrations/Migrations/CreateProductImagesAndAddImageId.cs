using FluentMigrator;

namespace OnlineShop.Migrations.Migrations;

[Migration(18, "Create ProductImages table and add ImageId to Products")]
public class CreateProductImagesAndAddImageId : Migration
{
    public override void Up()
    {
        Create.Table("ProductImages")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("FileName").AsString().NotNullable()
            .WithColumn("FilePath").AsString().NotNullable()
            .WithColumn("Size").AsInt64().NotNullable()
            .WithColumn("CreatedAt").AsDateTime().NotNullable();

        Alter.Table("Products")
            .AddColumn("ImageId").AsGuid().Nullable()
            .ForeignKey("FK_Products_ProductImages", "ProductImages", "Id");
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_Products_ProductImages").OnTable("Products");
        Delete.Column("ImageId").FromTable("Products");
        Delete.Table("ProductImages");
    }
}