using FluentMigrator;

namespace OnlineShop.Migrations.Migrations
{
    [Migration(12, "Create Products Table")]
    public class CreateProductsTable : Migration
    {
        public override void Up()
        {
            Create.Table("Products")
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("Sku").AsString().NotNullable().Unique()
                .WithColumn("Brand").AsString().NotNullable()
                .WithColumn("CategoryId").AsGuid().NotNullable()
                .ForeignKey("FK_Products_Categories", "Categories", "Id") // <-- here
                .WithColumn("ShortDescription").AsString().NotNullable()
                .WithColumn("LongDescription").AsString().NotNullable()
                .WithColumn("Price").AsInt32().NotNullable()
                .WithColumn("Image").AsString().Nullable()
                .WithColumn("Enabled").AsBoolean().NotNullable().WithDefaultValue(true);
        }

        public override void Down()
        {
            Delete.Table("Products");
        }
    }
}
